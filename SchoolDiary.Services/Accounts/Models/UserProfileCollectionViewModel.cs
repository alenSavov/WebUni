using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Accounts.Models
{
    public class UserProfileCollectionViewModel
    {
        public IEnumerable<UserProfileCourseViewModel> Courses { get; set; }

        public UserProfileViewModel Profile { get; set; }

        public IEnumerable<UserProfileEventsViewModel> Events { get; set; }
    }
}
