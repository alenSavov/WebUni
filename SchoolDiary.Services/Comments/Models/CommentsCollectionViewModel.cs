using System.Collections.Generic;

namespace SchoolDiary.Services.Comments.Models
{
    public class CommentsCollectionViewModel
    {
        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
