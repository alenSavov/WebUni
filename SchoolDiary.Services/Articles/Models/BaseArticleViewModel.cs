using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Services.Comments.Models;
using System;
using System.Collections.Generic;

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

        public int CommentsCounter { get; set; }

        public DateTime CreatedOn { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Article, BaseArticleViewModel>()
               .ForMember(x => x.ArticlePictureVersion, x => x.MapFrom(n => n.ArticleVersionPicture));
        }
    }
}
