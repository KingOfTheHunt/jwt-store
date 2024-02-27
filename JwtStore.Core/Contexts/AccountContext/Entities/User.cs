using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.SharedContext.Entities;

namespace JwtStore.Core.Contexts.AccountContext.Entities;

public class User : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; set; } = null!;
    public Password Password { get; set; } = null!;
    public string Image { get; set; } = string.Empty;
    public IList<Role> Roles { get; set; } = new List<Role>();

    public User()
    {
    }

    public User(string name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public User(string name, string email, string? password = null)
    {
        Name = name;
        Email = email;
        Password = new Password(password);
    }

    public void UpdatePassword(string plainTextPassword, string code)
    {
        if (string.Equals(code.Trim(), Password.ResetCode.Trim()) == false)
            throw new InvalidResetCodePasswordException("O código informado é inválido.");

        var password = new Password(plainTextPassword);
        Password = password; 
    }

    public void UpdateEmail(Email email) 
    {
        Email = email;
    }

    public void ChangePassword(string plainTextPassword)
    {
        var password = new Password(plainTextPassword);
        Password = password;
    }
}