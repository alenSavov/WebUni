namespace SchoolDiary.Models
{
    public class UsersCourses
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
