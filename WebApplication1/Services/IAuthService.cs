using WebApplication1.Models.DTO;

namespace WebApplication1.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(string name, string username, string password, string email, string phone);
    Task<string> LoginAsync(string username, string password);
    Task<List<RegisterDTO>> GetAllUsersAsync();
}
