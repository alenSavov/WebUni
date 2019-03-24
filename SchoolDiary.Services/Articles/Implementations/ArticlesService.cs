using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Services.Articles.Models;
using SchoolDiary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDiary.Services
{
    public class ArticlesService : IArticleService
    {
        private readonly SchoolDbContext _dbContext;

        public ArticlesService(SchoolDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Create(CreateArticleInputViewModel model, string imageUploadResult, string creatorName)
        {       
            var creator = _dbContext.Users
                .Where(u => u.UserName == creatorName).ToList();
            var fullName = "";
            foreach (var item in creator)
            {
                fullName = item.FirstName + " " + item.LastName;
            }

            var CreatedOn = DateTime.UtcNow;

            var article = new Article
            {
                Title = model.Title,
                Content = model.Content,
                Category = model.Category,
                CreatedOn = CreatedOn,
                Creator = fullName,
                ArticleVersionPicture = imageUploadResult
            };

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();
        }

        public IEnumerable<BaseArticleViewModel> GetAllArticles()
        {
            var articles = _dbContext.Articles
                .OrderByDescending(x => x.CreatedOn)
                .To<BaseArticleViewModel>()
                .ToList();

            return articles;
        }

        public BaseArticleViewModel GetById(string id)
        {
            var article = this._dbContext.Articles
               .Where(e => e.Id == id)
               .To<BaseArticleViewModel>()
               .FirstOrDefault();

            return article;
        }

        public void Delete(string id)
        {
            var article = this._dbContext.Articles
                .FirstOrDefault(a => a.Id == id);

            this._dbContext.Articles.Remove(article);
            this._dbContext.SaveChanges();
        }

        public void Edit(EditArticleInputModel model, string imageUploadResult)
        {
            var article = this._dbContext.Articles
                .FirstOrDefault(a => a.Id == model.Id);

           
            article.Title = model.Title;
            article.Content = model.Content;
            article.Category = model.Category;
            article.ArticleVersionPicture = imageUploadResult;

            this._dbContext.SaveChanges();
        }       

        public void Unfollow(string eventName, string username)
        {
            var user = this._dbContext.Users.FirstOrDefault(x => x.UserName == username);
            var eventToken = this._dbContext.Events.FirstOrDefault(x => x.Name == eventName);

            var userEvent = this._dbContext.UsersEvents.FirstOrDefault(x => x.EventId == eventToken.Id && x.UserId == user.Id);

            this._dbContext.UsersEvents.Remove(userEvent);
            this._dbContext.SaveChanges();
        }
    }
}
