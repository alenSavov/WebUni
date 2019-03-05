using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;

namespace SchoolDiary.Services.Articles.Models
{
    public class BaseArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Category { get; set; }

        public string ArticlePictureUrl { get; set; }

        public string ArticlePictureVersion { get; set; }

        public string Creator { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Article, BaseArticleViewModel>()
               .ForMember(x => x.ArticlePictureVersion, x => x.MapFrom(n => n.ArticleVersionPicture));
        }
    }
}
