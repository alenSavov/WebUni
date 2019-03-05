using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;

namespace SchoolDiary.Services.Accounts.Models
{
    public class UserProfileCourseViewModel : IMapFrom<UsersCourses>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
