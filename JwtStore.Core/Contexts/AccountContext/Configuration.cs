namespace JwtStore.Core.Contexts.AccountContext;

public static class Configuration
{
    public static DatabaseConfiguration Database { get; set; } = new();
    public static ConfigurationSecrets Secrets { get; private set; } = new();

    public class DatabaseConfiguration 
    {
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class ConfigurationSecrets 
    {
        public string ApiKey { get; set; } = string.Empty;
        public string JwtKey { get; set; } = string.Empty;
        public string PasswordSaltKey { get; set; } = string.Empty;
    } 
}