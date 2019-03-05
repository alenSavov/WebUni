using Microsoft.AspNetCore.Http;
using SchoolDiary.Mapping;
using SchoolDiary.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiary.Services.Events.Models
{
    public class EditEventViewModel : IMapFrom<Event>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Add new image")]
        public IFormFile EventPicture { get; set; }


    }
}
