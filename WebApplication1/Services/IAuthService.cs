using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services;

public interface IAuthService
{
    Task<IActionResult> RegisterAsync(string name, string username, string password, string email, string phone);
    Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(string username, string password);
    Task<List<RegisterDTO>> GetAllUsersAsync();
}
