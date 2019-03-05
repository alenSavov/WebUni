using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Administration.Models
{
    public class UsersViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<AllUsersAdministrationServiceViewModel> Users { get; set; }
    }
}
