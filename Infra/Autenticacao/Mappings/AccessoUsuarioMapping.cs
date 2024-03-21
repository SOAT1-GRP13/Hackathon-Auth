using Domain.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Autenticacao.Mappings;

public class AccessoUsuarioMapping : IEntityTypeConfiguration<AcessoUsuario>
{
    public void Configure(EntityTypeBuilder<AcessoUsuario> builder)
    {
        builder.ToTable("acesso_usuario");

        builder.Property(x => x.Matricula).HasColumnName("matricula");
        builder.Property(x => x.Senha).HasColumnName("senha");
        builder.Property(x => x.Nome).HasColumnName("nome");
        builder.Property(x => x.Email).HasColumnName("email");
    }
}
