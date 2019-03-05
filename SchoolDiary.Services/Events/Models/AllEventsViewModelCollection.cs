using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Events.Models
{
    public class AllEventsViewModelCollection
    {
        public IEnumerable<BaseEventViewModel> Events { get; set; }
    }
}
