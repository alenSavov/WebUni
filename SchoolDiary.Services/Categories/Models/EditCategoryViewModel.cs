using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Categories.Models
{
    public class EditCategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Add new image")]
        public IFormFile CategoryPicture { get; set; }
    }
}
