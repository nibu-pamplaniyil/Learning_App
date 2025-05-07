using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Migrations;
using WebApplication1.Models;
using WebApplication1.Services;
using System.Net;

public class ContactServicer:IContactServicer
{
    private readonly ApplicationDBContext _context;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public ContactServicer(ApplicationDBContext dbContext,IConfiguration configuration,IEmailService emailService)
    {
        _context = dbContext;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<Contact> Message(string name,string email,string subject,string message)
    {
        var contact = new Contact
        {
            Id = new Guid(),
            Name = name,
            Email = email,
            Subject = subject,
            Message = message
        };
        try{
            var fromAddress = _configuration["EmailSettings:SmtpUser"];
            var toAddress = _configuration["VerificationEmail:EmailVerify"];
            var fromPass = _configuration["EmailSettings:SmtpPass"];
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            string emailBody = $"Name: {name}\nEmail: {email}\nSubject: {subject}\nMessage: {message}";

            await _emailService.SendAsync(toAddress,subject,emailBody);

            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Email sending failed: {ex.Message}");
            throw;
        }
        
        try{
            _context.Contact.Add(contact);
            _context.SaveChanges();
        }
        catch(Exception e)
        {
            throw;
        }
        return contact;
    }
}