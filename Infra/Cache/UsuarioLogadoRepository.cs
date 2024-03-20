using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Domain.Cache;
using Microsoft.Extensions.Logging;

namespace Infra.Cache;

public class UsuarioLogadoRepository : IUsuarioLogadoRepository
{
    private readonly IDynamoDBContext _dynamoDBContext;
    private readonly ILogger _logger;
    public UsuarioLogadoRepository(IAmazonDynamoDB client, ILogger<UsuarioLogadoRepository> logger)
    {
        _logger = logger;
        _dynamoDBContext = new DynamoDBContext(client);
    }

    public async Task<bool> AddUsuarioLogado(string token)
    {
        try
        {
            var userLogin = new UsuarioLogado(token);
            await _dynamoDBContext.SaveAsync(userLogin);
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return false;

        }
    }
}
