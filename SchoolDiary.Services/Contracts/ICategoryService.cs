using Microsoft.AspNetCore.Http;
using SchoolDiary.Models;
using SchoolDiary.Services.Categories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolDiary.Services.Contracts
{
    public interface ICategoryService
    {
        Task<bool> Create(CreateCategoryInputViewModel model, string imageUploadResult);

        IEnumerable<CategoryViewModel> GetAll();

        IEnumerable<Category> GetAllCategoriesPlainCollection();

        void AddCourse(Course course);

        void DeleteCategory(string id);

        EditCategoryViewModel GetAllCategoryById(string id);

        void EditCategory(EditCategoryViewModel model, string imageUploadResult);
    }
}
