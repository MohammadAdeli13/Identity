using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PassWord { get; set; }
    }
}
