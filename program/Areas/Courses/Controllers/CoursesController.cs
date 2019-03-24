using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Data;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using SchoolDiary.ServicesViewModels.Courses;
using System.Linq;

namespace SchoolDiary.Web.Areas
{
    [Authorize]
    [Area(GlobalConstants.Areas.Courses)]
    public class CoursesController : Controller
    {
        private const string ManageCoursesPath = "/Administration/Administration/ManageCourses";
        private const string NoFoundCourseWithThisName = "Course with this name not found";
        private const string RedirectToCourseWithId = "Details?id=";

        private readonly SchoolDbContext _dbContext;
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly ICloudinaryService _cloudinaryService;

        public CoursesController(SchoolDbContext dbContext, ICourseService courseService, ICategoryService categoryService,
            ICloudinaryService cloudinaryService)
        {
            this._dbContext = dbContext;
            this._courseService = courseService;
            this._categoryService = categoryService;
            this._cloudinaryService = cloudinaryService;
        }

        [Authorize]
        public IActionResult AllById(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var coursesById = this._courseService
                .AllById(id)
                .ToList();

            foreach (var course in coursesById)
            {
                course.CoursePictureUrl = this._cloudinaryService.BuildCoursePictureUrl(course.Name, course.CoursePictureVersion);
            }

            var vm = new CollectionAllCoursesByidInCategoryViewModel
            {
                courses = coursesById
            };


            return this.View(vm);
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var courseDetails = this._courseService
                .Details(id);

            foreach (var course in courseDetails)
            {
                course.CoursePictureUrl = this._cloudinaryService.BuildCoursePictureUrl(course.Name, course.CourseVersionPicture);
            }

            return this.View(courseDetails);
        }

        [Authorize]
        public IActionResult GetTheCourse(string Id)
        {
            if (Id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var studentName = this.User.Identity.Name;

            var result = this._courseService.GetTheCourse(Id, studentName);

            if (!result)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = $"User with name: {studentName} already exist.";
                return Redirect("/");
            }
            else
            {
                TempData[GlobalConstants.TempDataSuccessMessageKey] = $"User with name: {studentName} is joined to the course.";
            }

            return Redirect("/");
        }

        [Authorize]
        public IActionResult UnFollowCourse(string courseName)
        {
            if (courseName == null)
            {
                return this.View();
            }

            var currentUserName = this.User.Identity.Name;

            this._courseService.UnFollowCourse(courseName, currentUserName);

            return this.Redirect("/account/profile?=" + this.User.Identity.Name);
        }


        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            this._courseService.DeleteCourse(id);

            return Redirect(ManageCoursesPath);
        }

        [HttpPost]
        public IActionResult SearchCourseByName(string courseName)
        {
            if (courseName == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
                return Redirect("/");
            }

            var course = this._courseService.SearchForCourseByName(courseName);

            if (course == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = NoFoundCourseWithThisName;
                return Redirect("/");
            }

            return Redirect(RedirectToCourseWithId + course.Id);
        }
    }
}