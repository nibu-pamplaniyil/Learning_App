using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IContactServicer
{
    public Task<Contact> Message(string Name,string Email,string Subject,string Message);
}