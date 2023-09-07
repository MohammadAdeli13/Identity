using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model.DTO
{
    public class UserRegasterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public string ConfirmPassWord { get; set; }
    }
}
