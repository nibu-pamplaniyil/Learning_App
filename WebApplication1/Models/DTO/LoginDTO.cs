using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }  
        
    }
}
