namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class InvalidVerificationCodeException(string? message) : Exception(message)
{
}