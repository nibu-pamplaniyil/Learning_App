using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using NETCore.MailKit.Core;

namespace WebApplication1.Services
{
    public class Auth : IAuthService
    {
        private readonly ApplicationDBContext _context;
        private readonly PasswordHasher<Register> _passwordHash;
        private readonly SignInManager<Register> _signinManager;
        private readonly UserManager<Register> _userManager;
        private readonly IEmailService _emailService;
        private static readonly ConcurrentDictionary<string, string> _otpStore = new ConcurrentDictionary<string, string>();
        private static readonly TimeSpan _otpExpirationTime = TimeSpan.FromMinutes(5);

        public Auth(ApplicationDBContext context, SignInManager<Register> signInManager, UserManager<Register> userManager, IEmailService emailService)
        {
            _context = context;
            _signinManager = signInManager;
            _passwordHash = new PasswordHasher<Register>();
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<IActionResult> RegisterAsync(string name, string username, string password, string email, string phone)
        {
            // Check if the user already exists
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist != null)
            {
                return new ConflictObjectResult("User already exist");
            }

            // Create the new user
            var user = new Register
            {
                UserName = username,
                Email = email,
                Name = name,
                Phone = phone
            };

            // Hash the password before storing it
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return new OkObjectResult("User created successfully");
            var error = result.Errors.Select(e => e.Description);
            return new BadRequestObjectResult(error);

        }

        public async Task<Microsoft.AspNetCore.Identity.SignInResult> LoginAsync(string username, string password)
        {
            // Find the user by username
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Microsoft.AspNetCore.Identity.SignInResult.Failed;
            }

            // Verify the password
            var verificationResult = await _signinManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            if (verificationResult.Succeeded)
                return verificationResult;
            return Microsoft.AspNetCore.Identity.SignInResult.Failed;

        }
        public async Task<List<RegisterDTO>> GetAllUsersAsync()
        {
            var users = await _context.Registers.Select(x => new RegisterDTO
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Email = x.Email,
                Phone = x.Phone
            }).ToListAsync();
            return users;
        }
        public async Task<bool> SendOTP(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            string otp = GenerateRandomOtp();
            string subject = "Your OTP Code For Login";
            string message = $"Your OTP code is: {otp}. It will expire in {_otpExpirationTime.TotalMinutes} minutes.";
            try
            {
                await _emailService.SendAsync(email, subject, message);
            }
            catch (Exception ex)
            {
                return false;
            }
            _otpStore.AddOrUpdate(email, otp, (key, oldValue) => otp);
            //var otpExpirationTask = Task.Delay(_otpExpirationTime);
            //await otpExpirationTask;
            //_otpStore.TryRemove(email, out _);
            _ = Task.Run(async () =>
            {
                await Task.Delay(_otpExpirationTime);
                _otpStore.TryRemove(email, out _);
            });
            return true;
        }
        private string GenerateRandomOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            return otp.ToString();
        }
    }
}
