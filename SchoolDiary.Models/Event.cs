using System;
using System.Collections.Generic;

namespace SchoolDiary.Models
{
    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<UsersEvents>();
            this.Resources = new List<EventsResources>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Place { get; set; }

        public string EventVersionPicture { get; set; }

        public ICollection<UsersEvents> Users { get; set; }

        public ICollection<EventsResources> Resources { get; set; }
    }
}
