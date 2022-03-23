using AuthenticationSystem.Helpers.SendGrid;

namespace AuthenticationSystem.Services
{
    public interface IEmailSender
    {
        AuthMessageSenderOptions Options { get; }

        Task Execute(string apiKey, string subject, string message, string toEmail);
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}