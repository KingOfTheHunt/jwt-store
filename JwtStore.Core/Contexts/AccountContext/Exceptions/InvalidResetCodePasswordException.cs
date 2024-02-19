namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class InvalidResetCodePasswordException(string? message) : Exception(message)
{
}