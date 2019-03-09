using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Services.Articles.Models;
using SchoolDiary.Services.Comments.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using System.Linq;

namespace SchoolDiary.Web.Controllers
{
    public class CommentsController : Controller
    {
        private const string ArticleDetailsPath = "/Articles/Articles/Details?id=";
        private const string ArticlesPath = "/Articles/Articles";

        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        public IActionResult AddComment(BaseArticleViewModel model, string id)
        {
            if (model.Comment == null || id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidData;

                return Redirect(ArticleDetailsPath + id);
            }

            var user = this.User.Identity.Name;

            this._commentService.AddComment(model.Comment, id, user);

            return Redirect(ArticlesPath);
        }

        public IActionResult DeleteCommentForArticle(string id)
        {
            if ( id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;

                return Redirect(ArticleDetailsPath + id);
            }

            this._commentService.DeleteCommentById(id);
            
            return Redirect(ArticlesPath);
        }

        //public IActionResult GetAllCommentsForArticle(string id)
        //{
        //    if (id == null)
        //    {
        //        TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;

        //        return Redirect(ArticlesPath);
        //    }

        //    var comments = this._commentService.GetAllComentsForArticle(id)
        //        .ToList();

        //    var commentsCollection = new CommentsCollectionViewModel
        //    {
        //        Comments = comments
        //    };

        //    return Redirect
        //}
    }
}
