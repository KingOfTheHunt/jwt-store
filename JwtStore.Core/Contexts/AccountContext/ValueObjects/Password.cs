using System.Security.Cryptography;
using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public class Password : ValueObject
{
    private const string VALID_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private const string SPECIAL_CHARS = "!@#$%&*{}[]()";

    public string Hash { get; } = string.Empty;
    public string ResetCode { get; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    protected Password() 
    {
    }

    public Password(string? text) 
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            text = Generate();

        Hash = Hashing(text);
    }

    private static string Generate(int length = 16, 
        bool includeSpecialChars = true, 
        bool upperCase = false)
    {
        var chars = includeSpecialChars ? (VALID_CHARS + SPECIAL_CHARS) : VALID_CHARS;
        var startRandom = upperCase ? 26 : 0;
        var index = 0;
        var result = new char[length];
        var random = new Random();

        while (index < length)
        {
            result[index] = chars[random.Next(startRandom, chars.Length)];
            index++;
        }

        return new string(result);
    }

    private static string Hashing(string password, short saltSize = 16, 
        short keySize = 32, int interations = 1000, char splitChar = '.')
    {
        if (string.IsNullOrEmpty(password))
            throw new PasswordNullOrEmptyException("A senha não deve ser vazia ou nula.");

        password += Configuration.Secrets.PasswordSaltKey;

        using var algorithm = new Rfc2898DeriveBytes(password, saltSize, 
            interations, HashAlgorithmName.SHA256);
        var key = Convert.ToBase64String(algorithm.GetBytes(keySize));
        var salt = Convert.ToBase64String(algorithm.Salt);

        return $"{interations}{splitChar}{salt}{splitChar}{key}";
    }

    private static bool Verify(string hash, string password, short keySize = 32,
        int interations = 1000, char splitChar = '.')
    {
        password += Configuration.Secrets.PasswordSaltKey;

        var parts = hash.Split(splitChar, 3);
        if (parts.Length != 3)
            return false;

        var hashInterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        if (hashInterations != interations)
            return false;

        using var algorithm = new Rfc2898DeriveBytes(password, salt, 
            interations, HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(keySize);

        return keyToCheck.SequenceEqual(key);
    }
}