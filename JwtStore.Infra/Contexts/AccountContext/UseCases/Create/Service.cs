using JwtStore.Core.Contexts;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace JwtStore.Infra.Contexts.AccountContext.UseCases.Create;

public class Service : IService
{
    public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
    {
        var client = new SendGridClient(Configuration.SendGrid.ApiKey);
        var from = new EmailAddress(Configuration.Email.DefaultEmail, Configuration.Email.DefaultSenderName);
        var subject = "Verifique a sua conta";
        var to = new EmailAddress(user.Email, user.Name);
        var content = $"Código de ativação: {user.Email.Verification.Code}";
        var message = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        await client.SendEmailAsync(message, cancellationToken);
    }
}