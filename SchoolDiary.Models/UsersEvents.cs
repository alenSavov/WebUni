namespace SchoolDiary.Models
{
    public class UsersEvents
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string EventId { get; set; }
        public Event Event { get; set; }
    }
}
