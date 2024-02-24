namespace JwtStore.Core.Contexts;

public static class Configuration
{
    public static DatabaseConfiguration Database { get; set; } = new();
    public static ConfigurationSecrets Secrets { get; private set; } = new();
    public static EmailConfiguration Email { get; set; } = new();
    public static SendGridConfiguration SendGrid { get; set; } = new();

    public class DatabaseConfiguration 
    {
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class SendGridConfiguration 
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class EmailConfiguration 
    {
        public string DefaultEmail { get; set; } = string.Empty;
        public string DefaultSenderName { get; set; } = string.Empty;
    }

    public class ConfigurationSecrets 
    {
        public string ApiKey { get; set; } = string.Empty;
        public string JwtKey { get; set; } = string.Empty;
        public string PasswordSaltKey { get; set; } = string.Empty;
    } 
}