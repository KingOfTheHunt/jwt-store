namespace JwtStore.Core.AccountContext;

public static class Configuration
{
    public static ConfigurationSecrets Secrets { get; private set; } = new();

    public class ConfigurationSecrets 
    {
        public string ApiKey { get; set; } = string.Empty;
        public string JwtKey { get; set; } = string.Empty;
        public string PasswordSaltKey { get; set; } = string.Empty;
    } 
}