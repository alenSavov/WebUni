using Microsoft.AspNetCore.Http;
using SchoolDiary.Models;
using SchoolDiary.Services.Courses.Models;
using SchoolDiary.ServicesViewModels.Courses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolDiary.Services.Contracts
{
    public interface ICourseService
    {
        Task<bool> Create(CreateCourseInputViewModel model, string imageUploadResult);

        IEnumerable<AllCoursesByidInCategoryViewModel> AllById(string id);

        List<string> GetNameById(string id);

        List<CourseDetailsViewModel> Details(string id);

        bool GetTheCourse(string id, string studentName);

        void AddResourceToCourseById(string id, Resource resource);

        IEnumerable<BaseCourseViewModel> GetAll();

        BaseCourseViewModel GetById(string id);

        void EditCourse(BaseCourseViewModel model, string imageUploadResult);

        void DeleteCourse(string id);

        CourseDetailsViewModel SearchForCourseByName(string courseName);

        CourseDetailsViewModel GetCourseByName(string courseName);
    }
}
