using System;
using System.Collections.Generic;

namespace SchoolDiary.Models
{
    public class Article
    {
        public Article()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new List<Comment>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
        
        public string Creator { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string ArticleVersionPicture { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
