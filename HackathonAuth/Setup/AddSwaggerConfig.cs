using HackathonAuth.SwaggerExamples;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace HackathonAuth.Setup;

[ExcludeFromCodeCoverage]
public static class AddSwaggerConfig
{
    public static void AddSwaggerGenConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen((SwaggerGenOptions c) =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            c.EnableAnnotations();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            } });

            c.CustomSchemaIds((Type x) => x.FullName);
            c.IncludeXmlComments(xmlPath);

            c.SchemaFilter<EnumSchemaFilter>();
            c.ExampleFilters();
        });
    }
}
