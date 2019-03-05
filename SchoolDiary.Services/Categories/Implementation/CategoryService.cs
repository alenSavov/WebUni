using AutoMapper;
using Microsoft.AspNetCore.Http;
using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Services.Categories.Models;
using SchoolDiary.Services.Contracts;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(SchoolDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<bool> Create(CreateCategoryInputViewModel model, string imageUploadResult)
        {
            if (model.Name == null ||
                model.Description == null)
            {
                return false;
            }

            var category = new Category
            {
                Name = model.Name,
                Description = model.Description,
                CategoryVersionPicture = imageUploadResult
            };

            this._dbContext.Categories.Add(category);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var categories = this._dbContext.Categories
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryPictureVersion = x.CategoryVersionPicture,
                    Courses = x.Courses
                })
                .ToList();

            return categories;
        }

        public IEnumerable<Category> GetAllCategoriesPlainCollection()
        {
            var categories = this._dbContext.Categories
                .To<Category>()
                .ToList();

            return categories;
        }

        public void AddCourse(Course course)
        {
            var category = this._dbContext.Categories.Where(c => c.Id == course.Name).ToList();

            foreach (var item in category)
            {
                item.Courses.Add(course);
            }
        }

        public void DeleteCategory(string id)
        {
            var category = this._dbContext.Categories
                .FirstOrDefault(c => c.Id == id);

            this._dbContext.Categories.Remove(category);
            this._dbContext.SaveChanges();
        }

        public EditCategoryViewModel GetAllCategoryById(string id)
        {
            var category = this._dbContext.Categories
                .Select(x => new EditCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                })
                .FirstOrDefault(x => x.Id == id);

            return category;
        }

        public void EditCategory(EditCategoryViewModel model, string imageUploadResult)
        {
            var category = this._dbContext.Categories
                .FirstOrDefault(x => x.Id == model.Id);

            category.CategoryVersionPicture = imageUploadResult;
            category.Name = model.Name;
            category.Description = model.Description;

            this._dbContext.SaveChanges();
        }

    }

}

