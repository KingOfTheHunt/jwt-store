using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

// O Specification válida a requisição.
public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(request.Name.Length, 120, "Name", "O nome deve conter no máximo 120 caracteres.")
            .IsGreaterOrEqualsThan(request.Name.Length, 3, "Name", "O nome deve conter no mínimo 3 caracteres")
            .IsEmail(request.Email, "Email", "E-mail inválido.")
            .IsLowerOrEqualsThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres.")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "Password", "A senha deve conter no mínimo 8 caracteres");

}