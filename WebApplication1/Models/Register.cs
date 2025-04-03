using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models;

public class Register :IdentityUser
{
    public string Name { get; set; }
    
    public string Phone { get; set; }
    public string Password { get; set; }
}
