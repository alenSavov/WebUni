using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolDiary.Models
{
    public class Category
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new List<Course>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryVersionPicture { get; set; }

        public ICollection<Course> Courses { get; set; } 
    }
}
