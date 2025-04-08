namespace WebApplication1.Services;

public interface IEmailService
{
    Task SendAsync(string email, string subject, string message);
}
