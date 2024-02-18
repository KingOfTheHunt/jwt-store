namespace JwtStore.Core.AccountContext.Exceptions;

public class InvalidEmailException(string? message) : Exception(message)
{
}