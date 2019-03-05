namespace SchoolDiary.Models
{
    public class EventsResources
    {
        public string EventId { get; set; }
        public Event Event { get; set; }

        public string ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
