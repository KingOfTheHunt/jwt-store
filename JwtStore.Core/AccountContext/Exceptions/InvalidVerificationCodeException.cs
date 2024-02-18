namespace JwtStore.Core.AccountContext.Exceptions;

public class InvalidVerificationCodeException(string? message) : Exception(message)
{
}