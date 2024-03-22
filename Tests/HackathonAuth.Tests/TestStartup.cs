using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using HackathonAuth.Setup;
using Infra.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HackathonAuth.Tests;

public class TestStartup
{
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        var myConfiguration = new Dictionary<string, string?>
        {
            {"ConnectionString", "User ID=postgres;Password=Teste123;Host=localhost;Port=15433;Database=auth;Pooling=true;"},
            {"AWS:Profile", "terraform"},
            {"AWS:Region", "us-west-2"}
        };

        var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();

        services.AddLogging();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddDbContext<AutenticacaoContext>(options =>
            options.UseNpgsql(configuration.GetSection("ConnectionString").Value));


        var awsOptions = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddScoped<IDynamoDBContext, DynamoDBContext>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        DependencyInjection.RegisterServices(services);

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}
