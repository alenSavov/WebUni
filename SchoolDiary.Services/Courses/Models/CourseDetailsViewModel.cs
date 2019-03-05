using AutoMapper;
using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;
using System.Collections.Generic;

namespace SchoolDiary.ServicesViewModels.Courses
{
    public class CourseDetailsViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CoursePictureUrl { get; set; }

        public string CourseVersionPicture { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string Trainer { get; set; }

        public List<IFormFile> Files { get; set; }

        public ICollection<User> Users { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Course, CourseDetailsViewModel>()
               .ForMember(c => c.CourseVersionPicture, x => x.MapFrom(n => n.Category.CategoryVersionPicture));
        }
    }
}
