namespace Mailer.Web.HubClients
{
    public interface IEmailHubClient
    {
        Task RefreshDraft(int count);
        Task RefreshTrash(int count);
        Task RefreshSent(int count);
    }
}
