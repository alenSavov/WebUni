using AutoMapper;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SchoolDiary.Services.Accounts.Models
{
    public class UserProfileViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Username { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(15)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string Email { get; set; }

        public ICollection<UsersCourses> Courses { get; set; }

        public ICollection<UsersEvents> Events { get; set; }

        public ICollection<Article> Articles { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration
                 .CreateMap<User, UserProfileViewModel>()
                 .ForMember(u => u.RegisteredOn, x => x.MapFrom(n => n.RegiredOn.ToLocalTime()));
        }
    }
}
