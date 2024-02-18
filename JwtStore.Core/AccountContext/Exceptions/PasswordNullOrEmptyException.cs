namespace JwtStore.Core.AccountContext.Exceptions;

public class PasswordNullOrEmptyException(string? message) : Exception(message)
{
}