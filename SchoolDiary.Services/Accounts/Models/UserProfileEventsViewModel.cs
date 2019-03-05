using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;

namespace SchoolDiary.Services.Accounts.Models
{
    public class UserProfileEventsViewModel : IMapFrom<UsersEvents>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                .CreateMap<UsersEvents, UserProfileEventsViewModel>()
                .ForMember(x => x.Name, n => n.MapFrom(k => k.Event.Name));
        }
    }
}
