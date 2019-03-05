using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Events.Models
{
    public class DetailsViewModelColletion
    {
        public BaseEventViewModel  Event { get; set; }

        public IEnumerable<string> Users { get; set; }
    }
}
