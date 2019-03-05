using AutoMapper;
using SchoolDiary.Models;
using SchoolDiary.Services.Articles.Models;

namespace SchoolDiary.Web.Infrastructure.Mapping
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<CreateArticleInputViewModel, Article>();
        }
    }
}
