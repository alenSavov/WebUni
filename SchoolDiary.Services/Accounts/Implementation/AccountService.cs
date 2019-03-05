using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Models.Enums;
using SchoolDiary.Services.Accounts.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SchoolDbContext _dbContext;

        public AccountService(UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              RoleManager<IdentityRole> roleManager,
                              SchoolDbContext dbContext)

        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
        }

        public async Task<bool> Create(string username, string password, string confirmPassword, string email, string firstName, string lastName)
        {
            if (username == null ||
                password == null ||
                confirmPassword == null ||
                email == null ||
                firstName == null ||
                lastName == null)
                return false;

            if (password != confirmPassword)
            {
                return false;
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                RegiredOn = DateTime.UtcNow
            };

            var userCreateResult = await this._userManager.CreateAsync(user, password);

            if (userCreateResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Adding user to role
                if (this._userManager.Users.Count() == 1)
                {
                    if (await this._roleManager.RoleExistsAsync(GlobalConstants.UserRoles.ADMINISTRATOR_ROLE))
                    {
                        await this._userManager.AddToRoleAsync(user, GlobalConstants.UserRoles.ADMINISTRATOR_ROLE);
                    }
                }
                else
                {
                    if (await this._roleManager.RoleExistsAsync(GlobalConstants.UserRoles.ROLE_STUDENT))
                    {
                        await this._userManager.AddToRoleAsync(user, GlobalConstants.UserRoles.ROLE_STUDENT);
                    }
                }
            }

            return true;
        }

        public  UserProfileViewModel Profile(string username)
        {
            var user = this._dbContext.Users
                 .Where(x => x.UserName == username)
                 .To<UserProfileViewModel>()
                 .FirstOrDefault();

            return user;
        }

        public IEnumerable<UserProfileCourseViewModel> GetUserCourses(string username)
        {
            var user = this._dbContext.Users
                .FirstOrDefault(x => x.UserName == username);

            var courses = this._dbContext.UsersCourses
                .Where(x => x.UserId == user.Id)
                .Select(x => new UserProfileCourseViewModel
                {
                    Name = x.Course.Name,
                    StartDate = x.Course.StartDate.ToLocalTime(),
                    EndDate = x.Course.EndDate.ToLocalTime()
                })
                .ToList();

            return courses;
        }

        public IEnumerable<UserProfileEventsViewModel> GetUserEvents(string username)
        {
            var user = this._dbContext.Users
               .FirstOrDefault(x => x.UserName == username);

            var courses = this._dbContext.UsersEvents
                .Where(x => x.UserId == user.Id)
                .Select(x => new UserProfileEventsViewModel
                {
                    Name = x.Event.Name,
                    StartDate = x.Event.StartDate.ToLocalTime(),
                    EndDate = x.Event.EndDate.ToLocalTime()
                })
                .ToList();

            return courses;
        }

        public void UpdateProfile(UserProfileViewModel model)
        {
            var user = this._dbContext.Users.FirstOrDefault(u => u.UserName == model.Username);

            var firstNameUserSb = new StringBuilder(user.FirstName).ToString();
            var firstNameModelUserSb = new StringBuilder(model.FirstName).ToString();

            var lastNameUserSb = new StringBuilder(user.LastName).ToString();
            var lastNameModelUserSb = new StringBuilder(model.LastName).ToString();
            
            var userFirstNameIsChanged = firstNameUserSb.Equals(firstNameModelUserSb);
            var userLastNameIsChanged = lastNameUserSb.Equals(lastNameModelUserSb);

            if (!userFirstNameIsChanged)
            {
                user.FirstName = model.FirstName;
            }

            if (!userLastNameIsChanged)
            {
                user.LastName = model.LastName;
            }

            if (!userFirstNameIsChanged || !userLastNameIsChanged)
            {
                this._dbContext.SaveChanges();
            }
        }
    }
}
