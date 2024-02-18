namespace JwtStore.Core.AccountContext.Exceptions;

public class ExpiredVerificationCodeException(string? message) : Exception(message)
{
}