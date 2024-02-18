namespace JwtStore.Core.AccountContext.Exceptions;

public class InvalidResetCodePasswordException(string? message) : Exception(message)
{
}