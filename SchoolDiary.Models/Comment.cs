using System;

namespace SchoolDiary.Models
{
    public class Comment
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
