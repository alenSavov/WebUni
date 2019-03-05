using SchoolDiary.Services.Administration.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services.Contracts
{
    public interface IAdministrationService
    {
        Task<IEnumerable<AllUsersAdministrationServiceViewModel>> AllAsync();
    }
}
