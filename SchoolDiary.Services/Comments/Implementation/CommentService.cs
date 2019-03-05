using SchoolDiary.Data;
using SchoolDiary.Models;
using SchoolDiary.Services.Comments.Models;
using SchoolDiary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolDiary.Services.Comments.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly SchoolDbContext _dbContext;

        public CommentService(SchoolDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void AddComment(string comment, string id, string username)
        {
            var user = this._dbContext.Users
                .FirstOrDefault(X => X.UserName == username);

            var newComment = new Comment
            {
                ArticleId = id,
                Content = comment,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };

            this._dbContext.Comments.Add(newComment);
            this._dbContext.SaveChanges();
        }

        public IEnumerable<CommentViewModel> GetAllComentsForArticle(string id)
        {
            var comments = this._dbContext.Comments
                .Where(x => x.ArticleId == id)
                .Select(x => new CommentViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    User = x.User.UserName,
                    CreatedOn = x.CreatedOn.ToLocalTime()
                })
                .OrderByDescending(X => X.CreatedOn)
                .ToList();

            return comments;
        }

        public void DeleteCommentById(string commentId)
        {
            var comment = this._dbContext.Comments
                .FirstOrDefault(c => c.Id == commentId);

            this._dbContext.Comments.Remove(comment);
            this._dbContext.SaveChanges();
        }
    }
}
