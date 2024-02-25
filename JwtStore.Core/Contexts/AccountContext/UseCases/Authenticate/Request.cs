using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public record Resquest(string Email, string Password) : IRequest<UseCases.Authenticate.Response>;