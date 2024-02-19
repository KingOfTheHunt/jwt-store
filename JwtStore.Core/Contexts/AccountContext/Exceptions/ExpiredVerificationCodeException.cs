namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class ExpiredVerificationCodeException(string? message) : Exception(message)
{
}