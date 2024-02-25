using Flunt.Notifications;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Validate;

public class Response : SharedContext.UseCases.Response
{
    public Response(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }
}