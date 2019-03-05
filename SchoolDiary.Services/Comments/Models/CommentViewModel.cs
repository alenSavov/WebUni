using System;

namespace SchoolDiary.Services.Comments.Models
{
    public class CommentViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
