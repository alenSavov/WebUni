using AutoMapper;
using SchoolDiary.Models;
using SchoolDiary.Services.Categories.Models;

namespace SchoolDiary.Web.Infrastructure.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryInputViewModel, Category>();
            CreateMap<EditCategoryViewModel, Category>();
        }
    }
}
