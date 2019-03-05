using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Web.Models.Account
{
    public class LoginInputViewModel
    {
        [Required(ErrorMessage = "The username is required!")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public bool RememberMe { get; set; }
    }
}
