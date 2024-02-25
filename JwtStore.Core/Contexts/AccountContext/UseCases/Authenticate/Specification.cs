using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Assert(Request resquest) =>
        new Contract<Notification>()
            .Requires()
            .IsLowerThan(resquest.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres.")
            .IsGreaterThan(resquest.Password.Length, 7, "Password", "A senha de conter no mínimo 8 caracteres")
            .IsEmail(resquest.Email, "Email", "E-mail inválido.");
}