using System.Text.RegularExpressions;
using JwtStore.Core.Contexts.AccountContext.Exceptions;
using JwtStore.Core.Contexts.SharedContext.ValueObjects;
using JwtStore.Core.SharedContext.Extensions;

namespace JwtStore.Core.Contexts.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    #region Constantes
    private const string PATTERN = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    #endregion

    #region Propriedades
    public string Address { get; } = string.Empty;
    public string Hash => Address.ToBase64(); 
    public Verification Verification { get; private set; } = new();
    #endregion

    public Email()
    {
    }
    
    public Email(string address)
    {
        if (string.IsNullOrEmpty(address))
            throw new InvalidEmailException("E-mail inválido");

        Address = address.Trim().ToLower();

        if (Address.Length < 5) 
            throw new InvalidEmailException("E-mail inválido");

        if (EmailRegex().IsMatch(Address) == false)
            throw new InvalidEmailException("E-mail inválido");
    }

    #region Implicit Operators
    public static implicit operator string(Email email) => 
        email.ToString();

    public static implicit operator Email(string address) =>
        new(address);
    #endregion

    #region Métodos
    public void ResendVerification() =>
        Verification = new();

    public override string ToString() => 
        Address.Trim().ToLower();

    [GeneratedRegex(PATTERN)]
    private static partial Regex EmailRegex();
    #endregion
}