namespace SchoolDiary.Models
{
    public class CoursesResources
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }

        public string ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
