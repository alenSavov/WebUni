using AutoMapper;
using SchoolDiary.Models;
using SchoolDiary.Services.Events.Models;

namespace SchoolDiary.Web.Infrastructure.Mapping
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventInputViewModel, Event>();
            CreateMap<EditEventViewModel, Event>();
        }
    }
}
