using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;


namespace WebApplication1.Controllers;
[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    public AuthController(IAuthService authService,IEmailService emailService,IHttpContextAccessor httpContextAccessor,IConfiguration configuration)
    {
        _authService = authService;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
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
            var email = _configuration["VerificationEmail:EmailVerify"];
            bool registerotp = true;
            if (await _authService.SendOTP(email,registerotp))
            {
                var registerData = JsonSerializer.Serialize(register);
                _httpContextAccessor.HttpContext.Session.SetString("PendingOtp", email);
                _httpContextAccessor.HttpContext.Session.SetString("RegisterData", registerData);
                return Ok("OTP sent to your email");
            }
            else
            {
                return BadRequest("Failed to send OTP");
            }
            
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
                    var registerotp = false;
                    if (await _authService.SendOTP(login.UserName,registerotp))
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

    [HttpPost("registerverify")]
    public async Task<IActionResult> RegisterVerify([FromBody] OtpDTO otpDTO)
    {
        try
        {
            var pendingOtp = _httpContextAccessor.HttpContext.Session.GetString("PendingOtp");
            var registerData = _httpContextAccessor.HttpContext.Session.GetString("RegisterData");
            if (pendingOtp == null || registerData == null)
            {
                return BadRequest("No pending OTP requests");
            }
            bool result = await _authService.VerifyOTP(pendingOtp, otpDTO.OTP);
            if (result)
            {
                var register = JsonSerializer.Deserialize<Register>(registerData);
                var Registerresult = await _authService.RegisterAsync(register.Name, register.UserName, register.Password, register.Email, register.Phone);
                if(Registerresult is ConflictObjectResult)
                {
                    _httpContextAccessor.HttpContext.Session.Remove("PendingOtp");
                    _httpContextAccessor.HttpContext.Session.Remove("RegisterData");
                    return BadRequest("User already exists...!");
                }
                _httpContextAccessor.HttpContext.Session.Remove("PendingOtp");
                _httpContextAccessor.HttpContext.Session.Remove("RegisterData");
                return Ok("OTP verified... User registered successfully");
            }
            else
            {
                return BadRequest("Invalid OTP");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("otpverify")]
    public async Task<IActionResult> OTPVerify([FromBody] OtpDTO otpDTO)
    {
        try
        {
            var pendingOtp = _httpContextAccessor.HttpContext.Session.GetString("PendingOtp");
            if (pendingOtp == null)
            {
                return BadRequest("No pending OTP requests");
            }
            bool result = await _authService.VerifyOTP(pendingOtp, otpDTO.OTP);
            if (result)
            {
                _httpContextAccessor.HttpContext.Session.Remove("PendingOtp");
                return Ok("OTP verified... Login successfully");
            }
            else
            {
                return BadRequest("Invalid OTP");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
