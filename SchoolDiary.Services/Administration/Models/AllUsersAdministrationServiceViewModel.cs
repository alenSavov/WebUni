using SchoolDiary.Mapping;
using SchoolDiary.Models;

namespace SchoolDiary.Services.Administration.Models
{
    public class AllUsersAdministrationServiceViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
