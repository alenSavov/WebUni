using Microsoft.AspNetCore.Http;
using SchoolDiary.Services.Events.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services.Contracts
{
    public interface IEventsService
    {
         Task<bool> CreateEvent(CreateEventInputViewModel model, string imageUploadResult);

        IEnumerable<BaseEventViewModel> GetAll();

        BaseEventViewModel GetById(string id);

        EditEventViewModel EditById(string id);

        bool AddUser(string id, string username);

        IEnumerable<string> GetAllUsersInEvent(string id);

        void Delete(string id);

        void Edit(EditEventViewModel model, string imageUploadResult);
    }
}
