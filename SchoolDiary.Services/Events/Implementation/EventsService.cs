using AutoMapper;
using Microsoft.AspNetCore.Http;
using SchoolDiary.Data;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Events.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolDiary.Services.Events.Implementation
{
    public class EventsService : IEventsService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly IMapper _mapper;

        public EventsService(SchoolDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<bool> CreateEvent(CreateEventInputViewModel model, string imageUploadResult)
        {
            var @event = new Event
            {
                Name = model.Name,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Place = model.Place,
                EventVersionPicture = imageUploadResult
            };

            this._dbContext.Events.Add(@event);
            await this._dbContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<BaseEventViewModel> GetAll()
        {
            var events = this._dbContext.Events
                .To<BaseEventViewModel>()
                .OrderBy(x => x.StartDate)
                .ToList();

            return events;
        }

        public BaseEventViewModel GetById(string id)
        {
            var item = this._dbContext.Events
                .To<BaseEventViewModel>()
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public EditEventViewModel EditById(string id)
        {
            var item = this._dbContext.Events
                .To<EditEventViewModel>()
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public bool AddUser(string id, string username)
        {
            var user = this._dbContext.Users
                .FirstOrDefault(u => u.UserName == username);

            var currentEvent = this._dbContext.Events
                .FirstOrDefault(e => e.Id == id);

            var userExist = this._dbContext.UsersEvents.Any(x => x.User.Id == user.Id && x.Event.Id == currentEvent.Id);
            if (userExist)
            {
                return false;
            }

            var userInEvent = new UsersEvents
            {
                UserId = user.Id,
                EventId = currentEvent.Id
            };

            this._dbContext.UsersEvents.Add(userInEvent);
            this._dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<string> GetAllUsersInEvent(string id)
        {
            var users = this._dbContext.UsersEvents
                .Where(u => u.EventId == id)
                .Select(X => new
                {
                    FullName = X.User.FirstName + " " + X.User.FirstName
                })
                .ToList();

            var usersNames = new List<string>();
            foreach (var user in users)
            {
                usersNames.Add(user.FullName);
            }

            return usersNames;
        }

        public void Delete(string id)
        {
            var currentEvent = this._dbContext
                .Events
                .FirstOrDefault(e => e.Id == id);

            this._dbContext.Events.Remove(currentEvent);
            this._dbContext.SaveChanges();
        }

        public void Edit(EditEventViewModel model, string imageUploadResult)
        {
            var currentEvent = this._dbContext.Events
                .FirstOrDefault(e => e.Id == model.Id);           

            currentEvent.Name = model.Name;
            currentEvent.StartDate = model.StartDate;
            currentEvent.EndDate = model.EndDate;
            currentEvent.Description = model.Description;
            currentEvent.EventVersionPicture = imageUploadResult;

            this._dbContext.SaveChanges();
        }

    }
}
