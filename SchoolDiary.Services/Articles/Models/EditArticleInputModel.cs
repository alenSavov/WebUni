using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Articles.Models
{
    public class EditArticleInputModel : IMapFrom<Article>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is Required.")]
        [Range(1, int.MaxValue)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Category is Required.")]
        public string Category { get; set; }

        //[Display(Name = "Image")]
        //[DataType(DataType.Upload)]
        //public byte[] Image { get; set; }

        [Required(ErrorMessage = "Add Image")]
        public IFormFile ArticlePicture { get; set; }
    }
}
