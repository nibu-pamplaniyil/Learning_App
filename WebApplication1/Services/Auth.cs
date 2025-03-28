using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Services;
public class Auth
{
    private readonly UserManager<Register> _userManager;
    private readonly PasswordHasher<Register> _passwordHasher = new PasswordHasher<Register>();
    private readonly ApplicationDBContext _context;
    public Auth(UserManager<Register> userManager, ApplicationDBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<string> RegisterAsync(string name, string username, string password, string email, string phone)
    {
        var userExist = await _userManager.FindByNameAsync(email);
        if (userExist != null)
        {
            return "User already exists!";
        }
        var user = new Register
        {
            Id = Guid.NewGuid(),
            Name = name,
            Username = username,
            Password = _passwordHasher.HashPassword(null, password),
            Email = email,
            Phone = phone
        };

        var loginEntry = new Login
        {
            Id = Guid.NewGuid(),
            Username = username,
            Password = _passwordHasher.HashPassword(null, password)
        };

        await _context.Logins.AddAsync(loginEntry);
        await _context.SaveChangesAsync();

        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return "User created successfully!";
        }
        else
        {
            return "User creation failed!";
        }
    }
}
