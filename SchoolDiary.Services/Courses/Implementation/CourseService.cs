using Microsoft.AspNetCore.Http;
using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Courses.Models;
using SchoolDiary.ServicesViewModels.Courses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public class CourseService : ICourseService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly ICategoryService _categoryService;

        public CourseService(SchoolDbContext dbContext, ICategoryService categoryService)
        {
            this._dbContext = dbContext;
            this._categoryService = categoryService;
        }

        public async Task<bool> Create(CreateCourseInputViewModel model, string imageUploadResult)
        {
            if (model.Name == null ||
                model.Description == null ||
                model.StartDate == null ||
                model.EndDate == null ||
                model.Trainer == null)
            {
                return false;
            }

            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                CategoryId = model.CategoryName,
                CourseVersionPicture = imageUploadResult,
                Trainer = model.Trainer
            };

            this._dbContext.Courses.Add(course);
            this._categoryService.AddCourse(course);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<AllCoursesByidInCategoryViewModel> AllById(string id)
        {
            var coursesById = this._dbContext.Courses
               .Where(c => c.CategoryId == id)
               .Select(x => new AllCoursesByidInCategoryViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   CoursePictureVersion = x.CourseVersionPicture
               })
               .ToList();

            return coursesById;
        }

        public List<string> GetNameById(string id)
        {
            var categoryName = this._dbContext
                .Categories
                .Where(x => x.Id == id)
                .Select(n => n.Name).ToList();

            return categoryName;
        }

        public List<CourseDetailsViewModel> Details(string id)
        {
            var courseDetails = this._dbContext.Courses
                .To<CourseDetailsViewModel>()
                .Where(c => c.Id == id)
                .ToList();

            return courseDetails;
        }

        public bool GetTheCourse(string courseId, string studentName)
        {
            var user = this._dbContext.Users
                .FirstOrDefault(u => u.UserName == studentName);

            var course = this._dbContext.Courses
                .FirstOrDefault(c => c.Id == courseId);

            var userExist = this._dbContext.UsersCourses.Any(x => x.User.Id == user.Id && x.Course.Id == courseId);
            if (userExist)
            {
                return false;
            }

            var usersCourses = new UsersCourses
            {
                UserId = user.Id,
                CourseId = course.Id
            };

            this._dbContext.UsersCourses.Add(usersCourses);
            this._dbContext.SaveChanges();

            return true;
        }

        public void AddResourceToCourseById(string id, Resource resource)
        {
            var course = _dbContext.Courses
                .FirstOrDefault(c => c.Id == id);


        }

        public IEnumerable<BaseCourseViewModel> GetAll()
        {
            var courses = this._dbContext.Courses
                .To<BaseCourseViewModel>()
                .ToList();

            return courses;
        }

        public BaseCourseViewModel GetById(string id)
        {
            var course = this._dbContext.Courses
                .To<BaseCourseViewModel>()
                .FirstOrDefault(c => c.Id == id);

            return course;
        }

        public void EditCourse(BaseCourseViewModel model, string imageUploadResult)
        {
            var course = this._dbContext.Courses
               .FirstOrDefault(x => x.Id == model.Id);           

            course.Name = model.Name;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.StartDate = model.EndDate;
            course.Category = model.Category;
            course.Trainer = model.Trainer;
            course.CourseVersionPicture = imageUploadResult;

            this._dbContext.SaveChanges();
        }

        public void DeleteCourse(string id)
        {
            var course = this._dbContext.Courses
                .FirstOrDefault(c => c.Id == id);

            this._dbContext.Courses.Remove(course);
            this._dbContext.SaveChanges();
        }

        public CourseDetailsViewModel GetCourseByName(string courseName)
        {
            var course = this._dbContext.Courses
                .To<CourseDetailsViewModel>()
                .FirstOrDefault(x => x.Name == courseName);

            return course;
        }

        public CourseDetailsViewModel SearchForCourseByName(string courseName)
        {
            var course = this._dbContext
                .Courses
                .To<CourseDetailsViewModel>()
                .FirstOrDefault(c => c.Name.Contains(courseName));

            return course;
        }
    }
}
