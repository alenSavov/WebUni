using SchoolDiary.Services.Comments.Models;
using System.Collections.Generic;

namespace SchoolDiary.Services.Contracts
{
    public interface ICommentService
    {
        void AddComment(string comment, string id, string username);

        IEnumerable<CommentViewModel> GetAllComentsForArticle(string id);

        void DeleteCommentById(string commentId);
    }
}
