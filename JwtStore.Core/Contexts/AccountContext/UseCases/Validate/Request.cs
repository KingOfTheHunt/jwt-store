using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Validate;

public record Request(string Email, string VerificationCode) : IRequest<Response>;