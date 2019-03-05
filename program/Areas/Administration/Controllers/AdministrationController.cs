using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolDiary.Models;
using SchoolDiary.Services.Administration.Models;
using SchoolDiary.Services.Articles.Models;
using SchoolDiary.Services.Categories.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Courses.Models;
using SchoolDiary.Services.Events.Models;
using SchoolDiary.Services.Utilities;
using SchoolDiary.ServicesViewModels.Courses;
using SchoolDiary.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Web.Areas.Administration.Controllers
{
    [Area(GlobalConstants.Areas.Administration)]
    [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
    public class AdministrationController : Controller
    {
        private const string CreateEventPath = "Events/CreateEvent";
        private const string ManageEventPath = "Events/ManageEvent";
        private const string EditEventPath = "Events/EditEvent";
        private const string AllEventsPath = "events/events";
        private const string CreateEventPathToAction = "CreateEvent";
        private const string RedirectToManageEvents = "ManageEvent";

        private const string CreateCategoryPath = "Categories/CreateCategory";
        private const string ManageCategoryPath = "Categories/ManageCategory";
        private const string EditCategoryPath = "Categories/EditCategory";
        private const string RedirectToManageCategory = "ManageCategory";
        private const string RediretToEditCategoryPath = "EditCategory";
        private const string CategoryEditSuccess = "The category is changed successfully";

        private const string CreateCoursePath = "Courses/CreateCourse";
        private const string ManageCoursesPath = "Courses/ManageCourses";
        private const string EditCoursesPath = "Courses/EditCourse";
        private const string CreateCoursePathToAction = "CreateCourse";
        private const string CreateCategoryInvalidRedirectPath = "CreateCategory";
        private const string RedirectToActionPath = "ManageCourses";
        private const string RedirectToEditCoursePath = "EditCourse";

        private const string ManageArticlePath = "Articles/ManageArticles";
        private const string EditArticlePath = "Articles/EditArticles";

        private const string userAddToRole = "users/AddToRole";

        private const string CourseAddedSuccessfully = "The course is added successfully";
        private const string CategoryAddedSuccessfully = "The category is added successfully";

        private readonly IAdministrationService _administrationUserService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IEventsService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        private readonly IArticleService _articleService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper mapper;

        public AdministrationController(IAdministrationService administrationUserService,
                                        RoleManager<IdentityRole> roleManager,
                                        UserManager<User> userManager,
                                        IEventsService eventService,
                                        ICourseService courseService,
                                        ICategoryService categoryService,
                                        IArticleService articleService,
                                        ICloudinaryService cloudinaryService,
                                        IMapper mapper)
        {
            this._administrationUserService = administrationUserService;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._eventService = eventService;
            this._courseService = courseService;
            this._categoryService = categoryService;
            this._articleService = articleService;
            this._cloudinaryService = cloudinaryService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return this.View();
        }


        // User Actions
        public async Task<IActionResult> AddToRole()
        {
            var allUsers = await this._administrationUserService.AllAsync();
            var roles = this._roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(userAddToRole, new UsersViewModel
            {
                Roles = roles,
                Users = allUsers
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel model)
        {
            var roleExists = await this._roleManager.RoleExistsAsync(model.Role);
            var user = await this._userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this._userManager.AddToRoleAsync(user, model.Role);

            TempData.AddAdminSuccessMessage($"The user {user.UserName} is added successfully to role {model.Role}");

            var currentArea = RouteData.DataTokens["area"];
            return RedirectToAction("AddToRole");
            //return RedirectToAction("Index", "Home", new { area = currentArea });
        }



        // Event Actions
        public IActionResult CreateEvent()
        {
            return this.View(CreateEventPath);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("events/CreateEvent", model);
            }

            int result = DateTime.Compare(model.StartDate, model.EndDate);

            if (result > 0)
            {
                TempData.AddAdminErrorMessage($"Invalid date {model.EndDate}");
                return this.View(CreateEventPath, model);
            }

            Event currentEvent;
            var file = model.EventPicture;
            var validExtension = IsValidImageFileExtensions(file);
            if (!validExtension)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(CreateEventPathToAction + model.Id);
            }

            if (model.Name == null ||
               model.Description == null ||
               model.StartDate == null ||
               model.EndDate == null ||
               model.Place == null
               )
            {

                return this.View(CreateEventPath, model);
            }

            currentEvent = this.mapper.Map<Event>(model);

            if (file != null)
            {
                var eventPictureId = string.Format(GlobalConstants.EventPicture, model.Name);
                var fileStream = file.OpenReadStream();

                var imageUploadResult = this._cloudinaryService.UploadPicture(currentEvent.GetType(), eventPictureId, fileStream);

                var eventCreateResult = await this._eventService.CreateEvent(model, imageUploadResult.Version);

                if (eventCreateResult)
                {
                    TempData[GlobalConstants.TempDataSuccessMessageKey] = $"The event {model.Name} is added successfully";

                    return RedirectToAction("ManageEvent");                    
                }
            }
            
            return RedirectToAction("index", "events", new { area = "Events" });
        }

        public IActionResult ManageEvent()
        {
            var events = this._eventService.GetAll()
                .ToList();

            var eventsCollection = new AllEventsViewModelCollection
            {
                Events = events
            };

            return this.View(ManageEventPath, eventsCollection);
        }

        public IActionResult EditEvent(string id)
        {
            var currentEvent = this._eventService.EditById(id);

            return this.View(EditEventPath, currentEvent);
        }

        [HttpPost]
        public IActionResult EditEvent(EditEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("events/EditEvent", model);
            }

            Event currentEvent;
            currentEvent = this.mapper.Map<Event>(model);
            if (model.EventPicture == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Image file is required";
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            int result = DateTime.Compare(model.StartDate, model.EndDate);

            if (result > 0)
            {
                TempData.AddAdminErrorMessage($"Invalid date {model.EndDate}");
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            var file = model.EventPicture;
            var validExtension = IsValidImageFileExtensions(file);
            if (!validExtension)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            var eventPictureId = string.Format(GlobalConstants.EventPicture, model.Name);
            var fileStream = file.OpenReadStream();

            var imageUploadResult = this._cloudinaryService.UploadPicture(currentEvent.GetType(), eventPictureId, fileStream);

            this._eventService.Edit(model, imageUploadResult.Version);

            TempData[GlobalConstants.TempDataSuccessMessageKey] = "The event is changed successfully";

            return RedirectToAction(RedirectToManageEvents);
        }



        // Category Actions
        public IActionResult CreateCategory()
        {
            return this.View(CreateCategoryPath);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Categories/CreateCategory", model);
            }

            if (model.Name == null ||
                model.Description == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
                return RedirectToAction(CreateCategoryInvalidRedirectPath);
            }

            Category category;
            var file = model.CategoryPicture;
            var currentFileExtention = file.FileName.Split('.')[1];
            var correctFilesExtentionCollection = new[] { GlobalConstants.FileExtensions.PngFormat,
                                                          GlobalConstants.FileExtensions.JpgFormat ,
                                                          GlobalConstants.FileExtensions.JpegFormat};

            if (!correctFilesExtentionCollection.Contains(currentFileExtention))
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return RedirectToAction(CreateCategoryInvalidRedirectPath);
            }

            category = this.mapper.Map<Category>(model);

            if (file != null)
            {
                var categoryPictureId = string.Format(GlobalConstants.CategoryPicture, model.Name);
                var fileStream = file.OpenReadStream();

                var imageUploadResult = this._cloudinaryService.UploadPicture(category.GetType(), categoryPictureId, fileStream);

                var categoryCreateResult = this._categoryService.Create(model, imageUploadResult.Version);

                if (await categoryCreateResult)
                {
                    TempData[GlobalConstants.TempDataSuccessMessageKey] = CategoryAddedSuccessfully;

                    return RedirectToAction(RedirectToManageCategory);
                }
            }

            return View(model);
        }


        public IActionResult EditCategory(string id)
        {
            var category = this._categoryService.GetAllCategoryById(id);

            return this.View(EditCategoryPath, category);
        }

        [HttpPost]
        public IActionResult EditCategory(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Categories/EditCategory", model);
            }

            if (model.Name == null ||
               model.Description == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            Category category;
            category = this.mapper.Map<Category>(model);
            if (model.CategoryPicture == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Image file is required";
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            var file = model.CategoryPicture;
            var currentFileExtention = file.FileName.Split('.')[1];
            var correctFilesExtentionCollection = new[] { GlobalConstants.FileExtensions.PngFormat,
                                                          GlobalConstants.FileExtensions.JpgFormat ,
                                                          GlobalConstants.FileExtensions.JpegFormat};

            if (!correctFilesExtentionCollection.Contains(currentFileExtention))
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            var categoryPictureId = string.Format(GlobalConstants.CategoryPicture, model.Name);
            var fileStream = file.OpenReadStream();

            var imageUploadResult = this._cloudinaryService.UploadPicture(category.GetType(), categoryPictureId, fileStream);

            this._categoryService.EditCategory(model, imageUploadResult.Version);

            TempData[GlobalConstants.TempDataSuccessMessageKey] = CategoryAddedSuccessfully;

            return RedirectToAction(RedirectToManageCategory);
        }

        public IActionResult ManageCategory()
        {
            var categories = this._categoryService.GetAll()
                .ToList();

            var categoriesCollection = new CategoryCollectionViewModel
            {
                Categories = categories
            };

            return this.View(ManageCategoryPath, categoriesCollection);
        }



        // Course Actions
        public IActionResult CreateCourse()
        {
            var categoryList = this._categoryService.GetAll();
            ViewBag.ListOfCategories = categoryList;

            return this.View(CreateCoursePath);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseInputViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                var categoryList = this._categoryService.GetAll();
                ViewBag.ListOfCategories = categoryList;
                return this.View(CreateCoursePath);
            }

            if (model.Name == null ||
               model.Description == null ||
               model.StartDate == null ||
               model.EndDate == null ||
               model.CategoryName == null ||
               model.Trainer == null ||
               model.CoursePicture == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Invalid data.";
                return RedirectToAction(CreateCoursePathToAction);
            }

            int result = DateTime.Compare(model.StartDate, model.EndDate);

            if (result > 0)
            {
                TempData.AddAdminErrorMessage($"Invalid date {model.EndDate}");
                return RedirectToAction(CreateCoursePathToAction);
            }

            Course course;
            var file = model.CoursePicture;
            var validExtension = IsValidImageFileExtensions(file);
            if (!validExtension)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            course = this.mapper.Map<Course>(model);

            if (file != null)
            {
                var coursePictureId = string.Format(GlobalConstants.CoursePicture, model.Name);
                var fileStream = file.OpenReadStream();

                var imageUploadResult = this._cloudinaryService.UploadPicture(course.GetType(), coursePictureId, fileStream);

                var courseCreateResult = this._courseService.Create(model, imageUploadResult.Version);

                if (await courseCreateResult)
                {
                    if (returnUrl == null)
                    {
                        TempData[GlobalConstants.TempDataSuccessMessageKey] = CourseAddedSuccessfully;

                        return RedirectToAction(RedirectToActionPath);
                    }

                    return RedirectToAction(returnUrl);
                }
            }

            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(model);
        }



        public IActionResult ManageCourses()
        {
            var courses = this._courseService.GetAll()
                .ToList();

            var coursesCollection = new AllCoursesViewModelCollection
            {
                Courses = courses
            };

            return this.View(ManageCoursesPath, coursesCollection);
        }

        public IActionResult EditCourse(string id)
        {
            var course = this._courseService.GetById(id);

            return this.View(EditCoursesPath, course);
        }

        [HttpPost]
        public IActionResult EditCourse(BaseCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Courses/EditCourse", model);
            }

            if (model.Name == null ||
               model.Description == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;
                return View(EditCoursesPath + model.Id, model);
            }

            Course course;
            course = this.mapper.Map<Course>(model);
            if (model.CoursePicture == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = "Image file is required";
                return View(EditCoursesPath + model.Id, model);
            }

            var file = model.CoursePicture;
            var validExtension = IsValidImageFileExtensions(file);
            if (!validExtension)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.WrongFileType;
                return Redirect(RediretToEditCategoryPath + model.Id);
            }

            var coursePictureId = string.Format(GlobalConstants.CoursePicture, model.Name);
            var fileStream = file.OpenReadStream();

            var imageUploadResult = this._cloudinaryService.UploadPicture(course.GetType(), coursePictureId, fileStream);

            this._courseService.EditCourse(model, imageUploadResult.Version);

            TempData[GlobalConstants.TempDataSuccessMessageKey] = "The course id changed successfully";

            return RedirectToAction(RedirectToActionPath);
        }



        // Article Actions
        public IActionResult ManageArticle()
        {
            var articles = this._articleService.GetAllArticles()
                .ToList();

            var articlesCollection = new AllArticlesCollectionViewModel
            {
                Articles = articles
            };

            return this.View(ManageArticlePath, articlesCollection);
        }

        public bool IsValidImageFileExtensions(IFormFile file)
        {
            var currentFileExtention = file.FileName.Split('.')[1];
            var correctFilesExtentionCollection = new[] { GlobalConstants.FileExtensions.PngFormat,
                                                          GlobalConstants.FileExtensions.JpgFormat ,
                                                          GlobalConstants.FileExtensions.JpegFormat};

            if (!correctFilesExtentionCollection.Contains(currentFileExtention))
            {
                return false;
            }

            return true;
        }

    }
}
