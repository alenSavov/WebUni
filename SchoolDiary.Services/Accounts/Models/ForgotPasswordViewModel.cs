using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Accounts.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
