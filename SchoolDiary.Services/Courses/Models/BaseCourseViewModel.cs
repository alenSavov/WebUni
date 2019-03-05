using AutoMapper;
using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.ServicesViewModels.Courses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Courses.Models
{
    public class BaseCourseViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Add new image")]
        public IFormFile CoursePicture { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public string Trainer { get; set; }
    }
}
