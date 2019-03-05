using System;
using System.Collections.Generic;

namespace SchoolDiary.Models
{
    public class Course
    {
        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<UsersCourses>();
            this.Resources = new List<CoursesResources>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CourseVersionPicture { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string Trainer { get; set; }

        public ICollection<CoursesResources> Resources { get; set; }

        public ICollection<UsersCourses> Users { get; set; }
        
    }
}
