using JwtStore.Core.AccountContext.Exceptions;
using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.SharedContext.Entities;

namespace JwtStore.Core.AccountContext.Entities;

public class User : Entity
{
    public string Name { get; private set; } = string.Empty;
    public Email Email { get; set; }
    public Password Password { get; set; }
    public string Image { get; set; } = string.Empty;

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