using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/contact")]
[ApiController]

public class ContactController : Controller
{
    private readonly IContactServicer _contactServicer;

    public ContactController(IContactServicer contactServicer)
    {
        _contactServicer = contactServicer;
    }

    [HttpPost]
    public async Task<IActionResult> Message([FromBody] Contact contact) 
    {
        try
        {
            var result = await _contactServicer.Message(contact.Name,contact.Email,contact.Subject,contact.Message);
            if(result!=null)
                return Ok(new{Message=result});
            return BadRequest("Sending is failed");
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}