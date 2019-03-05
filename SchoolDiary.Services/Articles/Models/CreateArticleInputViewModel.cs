using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Articles.Models
{
    public class CreateArticleInputViewModel
    {
        [Required]
        public string Title { get; set; }


        [Required]
        public string Content { get; set; }

        [Required]
        public string Category { get; set; }

        [Required(ErrorMessage = "Add Image")]
        public IFormFile ArticlePicture { get; set; }
    }
}
