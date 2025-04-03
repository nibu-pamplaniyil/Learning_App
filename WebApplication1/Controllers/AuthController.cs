using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost()]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
        var result = await _authService.RegisterAsync(register.Name, register.UserName, register.Password, register.Email, register.Phone);
        if(result== "User created successfully!")
            return Ok(new {message = result});
        return BadRequest(new {message = result });
    }

    [HttpGet()]
    public async Task<List<RegisterDTO>> GetAllUsers()
    {
        var result = await _authService.GetAllUsersAsync();
        return result;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var result = await _authService.LoginAsync(loginDTO.UserName, loginDTO.Password);
        if (result == "User logged in!")
            return Ok(new { message = result });
        return BadRequest(new { message = result });
    }

}
