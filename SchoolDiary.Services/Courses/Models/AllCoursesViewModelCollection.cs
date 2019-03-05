using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Courses.Models
{
    public class AllCoursesViewModelCollection
    {
        public IEnumerable<BaseCourseViewModel> Courses { get; set; }
    }
}
