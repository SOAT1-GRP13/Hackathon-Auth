﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HackathonAuth.Setup;

[ExcludeFromCodeCoverage]
public static class AddAuthenticationConfig
{
    public static void AddAuthenticationJWT(this IServiceCollection services, string secret)
    {
        var key = Encoding.ASCII.GetBytes(secret);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                    };
                });
    }
}
