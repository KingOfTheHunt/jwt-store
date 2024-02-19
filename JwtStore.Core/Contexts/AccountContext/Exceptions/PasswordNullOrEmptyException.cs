namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class PasswordNullOrEmptyException(string? message) : Exception(message)
{
}