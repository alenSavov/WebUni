using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Data;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Events.Models;
using SchoolDiary.Services.Utilities;

namespace SchoolDiary.Web.Areas.Events.Controllers
{
    [Area(GlobalConstants.Areas.Events)]
    [Authorize]
    public class EventsController : Controller
    {
        private const string ManageEventPath = "/Administration/Administration/ManageEvent";

        private readonly SchoolDbContext _dbContext;
        private readonly IEventsService _eventService;
        private readonly ICloudinaryService _cloudinaryService;

        public EventsController(SchoolDbContext dbContext, IEventsService eventService,
            ICloudinaryService cloudinaryService)
        {
            this._dbContext = dbContext;
            this._eventService = eventService;
            this._cloudinaryService = cloudinaryService;
        }

        public IActionResult Index()
        {
            var events = this._eventService.GetAll();

            foreach (var item in events)
            {
                item.EventPictureUrl = this._cloudinaryService.BuildEventPictureUrl(item.Name, item.EventPictureVersion);
            }

            var eventsViewModel = new AllEventsViewModelCollection
            {
                Events = events
            };

            return this.View(eventsViewModel);
        }

        public IActionResult Details(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var item = this._eventService
                .GetById(id);
            
            var users = this._eventService.GetAllUsersInEvent(id);

           item.EventPictureUrl = this._cloudinaryService.BuildEventPictureUrl(item.Name, item.EventPictureVersion);

            var detailsViewModel = new DetailsViewModelColletion
            {
                Event = item,
                Users = users
            };

            return this.View(detailsViewModel);
        }

        public IActionResult AddUser(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            var username = this.User.Identity.Name;

            var result = this._eventService.AddUser(id, username);
            if (!result)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = $"User with name: {username} already exist.";
                return Redirect("/");
            }
            else
            {
                TempData[GlobalConstants.TempDataSuccessMessageKey] = $"User with name: {username} is joined to the event.";
            }


            TempData[GlobalConstants.TempDataSuccessMessageKey] = $"User {username} is added to Event.";

            return Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            this._eventService.Delete(id);

            return Redirect(ManageEventPath);
        }

        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = GlobalConstants.InvalidId;
                return Redirect("/");
            }

            this._eventService.GetById(id);

            return Redirect("/");
        }
    }
}
