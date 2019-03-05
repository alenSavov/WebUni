using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;

namespace SchoolDiary.Services.Events.Models
{
    public class BaseEventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Place { get; set; }

        public string EventPictureVersion { get; set; }

        public string EventPictureUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
              .CreateMap<Event, BaseEventViewModel>()
              .ForMember(c => c.EventPictureVersion, x => x.MapFrom(n => n.EventVersionPicture));
        }
    }
}
