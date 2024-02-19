namespace JwtStore.Core.Contexts.AccountContext.Exceptions;

public class VerificationException(string? message) : Exception(message)
{
}