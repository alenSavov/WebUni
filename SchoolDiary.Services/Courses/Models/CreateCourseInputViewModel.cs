using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.ServicesViewModels.Courses
{
    public class CreateCourseInputViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        [Required (ErrorMessage = "Add image")]
        public IFormFile CoursePicture { get; set; }

        [Required(ErrorMessage = "Trainer name is Required!")]
        [Display(Name = "Trainer")]
        public string Trainer { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<Course, CreateCourseInputViewModel>()
                .ForMember(c => c.CategoryName, x => x.MapFrom(n => n.Category.Id))
                .ForMember(c => c.Categories, x => x.MapFrom(n => n.Category));
        }
    }
}
