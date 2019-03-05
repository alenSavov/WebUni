using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Categories.Models
{
    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Description { get; set; }

        public string CategoryPictureVersion { get; set; }

        public string CategoryPictureUrl { get; set; }

        public int CountCoursesInCategory { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
