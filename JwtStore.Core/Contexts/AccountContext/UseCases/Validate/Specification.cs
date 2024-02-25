using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Validate;

public static class Specification
{
    public static Contract<Notification> Assert(Request request) =>
        new Contract<Notification>()
            .Requires()
            .AreEquals(request.VerificationCode.Length, 6, "VericationCode", "O código deve conter seis caracteres.")
            .IsEmail(request.Email, "Email", "E-mail inválido.");
}