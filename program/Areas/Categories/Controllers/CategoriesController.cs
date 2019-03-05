using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Services.Categories;
using SchoolDiary.Services.Categories.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;

namespace SchoolDiary.Web.Controllers
{
    [Authorize]
    [Area(GlobalConstants.Areas.Categories)]
    public class CategoriesController : Controller
    {
        private const string ManageCategoriesPath = "/Administration/Administration/ManageCategory";

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        public IActionResult GetAll()
        {
            var categories = this._categoryService.GetAll();

            var model = new CategoryCollectionViewModel
            {
                Categories = categories
            };

            return this.View("/Home/Index");
        }

        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            this._categoryService.DeleteCategory(id);

            return Redirect(ManageCategoriesPath);
        }

        
    }
}
