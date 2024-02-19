namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class InvalidEmailException(string? message) : Exception(message)
{
}