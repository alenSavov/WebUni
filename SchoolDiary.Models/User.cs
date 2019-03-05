using Microsoft.AspNetCore.Identity;
using SchoolDiary.Models.Enums;
using System;
using System.Collections.Generic;

namespace SchoolDiary.Models
{
    public class User : IdentityUser
    {

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Courses = new List<UsersCourses>();
            this.Events = new List<UsersEvents>();
            this.Articles = new List<Article>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime RegiredOn { get; set; }

        public int? FacultyNumber { get; set; }

        public byte[] Image { get; set; }

        public ICollection<UsersCourses> Courses { get; set; }

        public ICollection<UsersEvents> Events { get; set; }

        public ICollection<Article> Articles { get; set; }
    }
}
