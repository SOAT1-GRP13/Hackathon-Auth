namespace Domain.Configuration;

public class Secrets
{
    public Secrets()
    {
        ClientSecret = string.Empty;
        PreSalt = string.Empty;
        PosSalt = string.Empty;
        ConnectionString = string.Empty;
    }

    public string ClientSecret { get; set; }
    public string PreSalt { get; set; }
    public string PosSalt { get; set; }
    public string ConnectionString { get; set; }
}
