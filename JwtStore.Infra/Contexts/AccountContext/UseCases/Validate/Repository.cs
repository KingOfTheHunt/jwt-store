using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Validate.Contracts;
using JwtStore.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Validate;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) => 
        await _context.Users
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);

    public async Task ValidateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}