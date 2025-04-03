using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class Auth : IAuthService
    {
        private readonly ApplicationDBContext _context;
        private readonly PasswordHasher<Register> _passwordHash;
        private readonly SignInManager<Register> _signinManager;
        private readonly UserManager<Register> _userManager;

        public Auth(ApplicationDBContext context, SignInManager<Register> signInManager, UserManager<Register> userManager)
        {
            _context = context;
            _signinManager = signInManager;
            _passwordHash = new PasswordHasher<Register>();
            _userManager = userManager;
        }

        public async Task<string> RegisterAsync(string name, string username, string password, string email, string phone)
        {
            // Check if the user already exists
            var userExist = await _userManager.FindByEmailAsync(email);
            if (userExist != null)
            {
                return "User already exists!";
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
                return "User created successfully!";
            return "Failed to create user!";

        }

        public async Task<string> LoginAsync(string username, string password)
        {
            // Find the user by username
            var user = await _context.Registers.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null)
            {
                return "User not found!";
            }

            // Verify the password
            var verificationResult = await _signinManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
            if (verificationResult.Succeeded)
            {
                return "User logged in!";
            }

            return "Invalid credentials!";
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
    }
}
