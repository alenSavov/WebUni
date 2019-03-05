using AutoMapper;
using SchoolDiary.Models;
using SchoolDiary.Services.Courses.Models;
using SchoolDiary.ServicesViewModels.Courses;

namespace SchoolDiary.Web.Infrastructure.Mapping
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseInputViewModel, Course>();
            CreateMap<BaseCourseViewModel, Course>();
        }
    }
}
