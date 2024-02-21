namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class InvalidRequestException(string? message) : Exception(message)
{
}