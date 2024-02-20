using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

// O Specification válida a requisição.
public static class Specification
{
    public static Contract<Notification> Assert(Resquest resquest) =>
        new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(resquest.Name.Length, 120, "Name", "O nome deve conter no máximo 120 caracteres.")
            .IsGreaterOrEqualsThan(resquest.Name.Length, 3, "Name", "O nome deve conter no mínimo 3 caracteres")
            .IsEmail(resquest.Email, "Email", "E-mail inválido.")
            .IsLowerOrEqualsThan(resquest.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres.")
            .IsGreaterOrEqualsThan(resquest.Password.Length, 8, "Password", "A senha deve conter no mínimo 8 caracteres");

}