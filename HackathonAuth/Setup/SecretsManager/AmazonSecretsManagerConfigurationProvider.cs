﻿using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace HackathonAuth.Setup.SecretsManager;

[ExcludeFromCodeCoverage]
public class AmazonSecretsManagerConfigurationProvider : ConfigurationProvider
{
    private readonly string _region;
    private readonly string _secretName;


    public AmazonSecretsManagerConfigurationProvider(string region, string secretName)
    {
        _region = region;
        _secretName = secretName;
    }

    public override void Load()
    {
        var secret = GetSecret();

        Data = JsonSerializer.Deserialize<Dictionary<string, string?>>(secret) ?? throw new InvalidOperationException("Secret era inválido");
    }

    private string GetSecret()
    {
        using var client =
        new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(_region));

        var request = new GetSecretValueRequest
        {
            SecretId = _secretName,
            VersionStage = "AWSCURRENT" // VersionStage defaults to AWSCURRENT if unspecified.
        };

        var response = client.GetSecretValueAsync(request).Result;

        string secretString;
        if (response.SecretString != null)
        {
            secretString = response.SecretString;
        }
        else
        {
            var memoryStream = response.SecretBinary;
            var reader = new StreamReader(memoryStream);
            secretString = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
        }

        return secretString;
    }
}
