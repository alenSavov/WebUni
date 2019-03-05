using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Web.Models.Account
{
    public class RegisterInputViewModel : IMapFrom<User>
    {
        [Required]
        [StringLength(15, MinimumLength = 3)]
        [Display(Name = "Username")]
        [RegularExpression(@"(\S)+", ErrorMessage = " White Space is not allowed in Usernames")]
        [ScaffoldColumn(false)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last name is Required.")]
        [MinLength(2)]
        [MaxLength(15)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Last name is Required.")]
        [MinLength(5)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
