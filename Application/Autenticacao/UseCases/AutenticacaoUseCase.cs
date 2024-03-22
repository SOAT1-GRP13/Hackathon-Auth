using Application.Autenticacao.Boundaries.Usuario;
using Application.Autenticacao.Dto.Usuario;
using Domain.Autenticacao;
using Domain.Cache;
using Domain.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Autenticacao.UseCases;

public class AutenticacaoUseCase : IAutenticacaoUseCase
{
    private readonly IAutenticacaoRepository _autenticacaoRepository;
    private readonly IUsuarioLogadoRepository _UsuarioLogadoRepository;
    private readonly Secrets _settings;

    public AutenticacaoUseCase(IAutenticacaoRepository autenticacaoRepository,
     IUsuarioLogadoRepository usuarioLogadoRepository,
      IOptions<Secrets> options)
    {
        _autenticacaoRepository = autenticacaoRepository;
        _UsuarioLogadoRepository = usuarioLogadoRepository;
        _settings = options.Value;
    }

    public async Task<AutenticaUsuarioOutput> AutenticaUsuario(IdentificaDto input)
    {
        var usuario = new AcessoUsuario(input.Matricula, input.Senha);

        var autenticado = await _autenticacaoRepository.AutenticaUsuario(usuario);

        if (string.IsNullOrEmpty(autenticado.Matricula))
            return new AutenticaUsuarioOutput();

        var token = GenerateToken(autenticado.Nome, autenticado.Role.ToString(), autenticado.Id, autenticado.Email);

        await _UsuarioLogadoRepository.AddUsuarioLogado(token);
        return new AutenticaUsuarioOutput(autenticado.Nome, token);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _autenticacaoRepository.Dispose();
    }

    private string GenerateToken(string name, string role, Guid idUsuario, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _settings.ClientSecret;
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string EncryptPassword(string dataToEncrypt)
    {
        string encryptedData;
        var bytes = Encoding.UTF8.GetBytes($"{_settings.PreSalt}{dataToEncrypt}{_settings.PosSalt}");
        var hash = SHA512.HashData(bytes);
        encryptedData = GetStringFromHash(hash);

        return encryptedData;
    }

    private static string GetStringFromHash(byte[] hash)
    {
        var result = new StringBuilder();

        for (var i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }

        return result.ToString();
    }
}
