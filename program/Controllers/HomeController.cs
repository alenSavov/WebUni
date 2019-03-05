using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using program.Models;
using SchoolDiary.Services.Categories.Models;
using SchoolDiary.Services.Contracts;

namespace program.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICloudinaryService _cloudinaryService;

        public HomeController(ICategoryService categoryService,
                              ICloudinaryService cloudinaryService)
        {
            this._categoryService = categoryService;
            this._cloudinaryService = cloudinaryService;
        }

      
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {

                var categories = this._categoryService.GetAll();

                foreach (var category in categories)
                {
                    category.CategoryPictureUrl = this._cloudinaryService.BuildCategoryPictureUrl(category.Name, category.CategoryPictureVersion);
                }

                var modelCollection = new CategoryCollectionViewModel
                {
                    Categories = categories
                };

                return View("Index-LoggedInUser", modelCollection);
            }

            return this.View();

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
