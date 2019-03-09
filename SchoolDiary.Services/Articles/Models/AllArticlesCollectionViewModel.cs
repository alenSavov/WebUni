using SchoolDiary.Services.Comments.Models;
using System.Collections.Generic;

namespace SchoolDiary.Services.Articles.Models
{
    public class AllArticlesCollectionViewModel
    {
        public ICollection<BaseArticleViewModel> Articles { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
