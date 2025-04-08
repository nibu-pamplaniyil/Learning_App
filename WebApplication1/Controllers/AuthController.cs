using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace WebApplication1.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthController(IAuthService authService,IEmailService emailService,IHttpContextAccessor httpContextAccessor)
    {
        _authService = authService;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
        try
        {
            if (string.IsNullOrEmpty(register.UserName) || string.IsNullOrEmpty(register.Password) || string.IsNullOrEmpty(register.Email))
            {
                return BadRequest(new { message = "Username, Email and Password are required!" });
            }
            var result = await _authService.RegisterAsync(register.Name, register.UserName, register.Password, register.Email, register.Phone);
            if (result == null)
                return BadRequest("Registration Failed");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<List<RegisterDTO>> GetAllUsers()
    {
        var result = await _authService.GetAllUsersAsync();
        return result;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO login)
    {
        try
        {
            var result = await _authService.LoginAsync(login.UserName, login.Password);
            if (result.Succeeded)
            {
                //return Ok("User logged in!");
                if (login.requestOTP)
                {
                    if (await _authService.SendOTP(login.UserName))
                    {
                        _httpContextAccessor.HttpContext.Session.SetString("PendingOtp", login.UserName);
                        return Ok("OTP sent to your email");
                    }
                    else
                    {
                        return BadRequest("Failed to send OTP");
                    }
                }
                else
                {
                    return Ok("User logged in!");
                }
            }
            else
                return Unauthorized("Invalid username or password");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
