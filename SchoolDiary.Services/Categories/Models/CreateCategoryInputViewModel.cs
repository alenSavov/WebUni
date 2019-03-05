using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Categories.Models
{
    public class CreateCategoryInputViewModel
    {
        [Required]
        [MinLength(1), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public IFormFile CategoryPicture { get; set; }

    }
}
