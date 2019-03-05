using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Events.Models
{
    public class CreateEventInputViewModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum length is 2 sign.")]
        [MaxLength(40, ErrorMessage ="Maximum length is 40 sign.")]
        public string Name { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Minimum length is 10 sign.")]
        [MaxLength(1000, ErrorMessage = "Maximum length is 1000 sign.")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Minimum length is 2 sign.")]
        [MaxLength(40, ErrorMessage = "Maximum length is 40 sign.")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        public IFormFile EventPicture { get; set; }
    }
}
