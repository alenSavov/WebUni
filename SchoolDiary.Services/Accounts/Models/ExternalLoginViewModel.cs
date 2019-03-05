using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "Username")]
        [RegularExpression(@"(\S)+", ErrorMessage = " White Space is not allowed in Usernames")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "И-мейл")]
        public string Email { get; set; }
    }
}
