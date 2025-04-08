using NETCore.MailKit.Core;
using System.Net;
using System.Net.Mail;

namespace WebApplication1.Services;


public class EmailServicer:IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailServicer(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SendAsync(string email, string subject, string message)
    {
        var fromEmail = _configuration["EmailSettings:FromEmail"];
        var smtpHost = _configuration["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUser = _configuration["EmailSettings:SmtpUser"];
        var smtpPass = _configuration["EmailSettings:SmtpPass"];
        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = message,
            IsBodyHtml = false,
        };

        mailMessage.To.Add(email);

        using var smtpClient = new SmtpClient
        {
            Host = smtpHost,
            Port = smtpPort,
            EnableSsl = true,
            Credentials = new NetworkCredential(smtpUser, smtpPass)
        };

        await smtpClient.SendMailAsync(mailMessage);

    }
}

