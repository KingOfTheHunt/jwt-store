using JwtStore.Core.Contexts.AccountContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Validate.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task ValidateAsync(User user);    
}