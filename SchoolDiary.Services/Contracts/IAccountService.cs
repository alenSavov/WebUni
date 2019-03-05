using SchoolDiary.Services.Accounts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SchoolDiary.Services.Contracts
{
    public interface IAccountService
    {
        Task<bool> Create(string username, string password, string confirmPassword, string email,string firstName, string LastName);

        UserProfileViewModel Profile(string username);

        IEnumerable<UserProfileCourseViewModel> GetUserCourses(string username);

        IEnumerable<UserProfileEventsViewModel> GetUserEvents(string username);

        void UpdateProfile(UserProfileViewModel model);
    }
}
