using SchoolDiary.Services.Comments.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Articles.Models
{
    public class DetailsViewModelCollection
    {
        public BaseArticleViewModel Article { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
