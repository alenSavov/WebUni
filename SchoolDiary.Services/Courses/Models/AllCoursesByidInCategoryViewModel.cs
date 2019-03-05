using SchoolDiary.Mapping;
using SchoolDiary.Models;

namespace SchoolDiary.ServicesViewModels.Courses
{
    public class AllCoursesByidInCategoryViewModel : IMapFrom<Course>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CoursePictureVersion { get; set; }

        public string CoursePictureUrl { get; set; }
    }
}
