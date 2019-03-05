using Microsoft.EntityFrameworkCore;
using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Services.Administration.Models;
using SchoolDiary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly SchoolDbContext _dbContext;

        public AdministrationService(SchoolDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<AllUsersAdministrationServiceViewModel>> AllAsync()
          => await this._dbContext
            .Users
            .To<AllUsersAdministrationServiceViewModel>()
            .ToListAsync();
    }
}
