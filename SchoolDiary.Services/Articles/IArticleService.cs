using Microsoft.AspNetCore.Http;
using SchoolDiary.Services.Articles.Models;
using System.Collections.Generic;

namespace SchoolDiary.Services.Contracts
{
    public interface IArticleService
    {
        void Create(CreateArticleInputViewModel model, string imageUploadResult, string creatorName);

        IEnumerable<BaseArticleViewModel> GetAllArticles();

        BaseArticleViewModel GetById(string id);

        void Delete(string id);

        void Edit(EditArticleInputModel model, string imageUploadResult);
    }
}
