using Microsoft.AspNetCore.Authorization;
using SchoolDiary.Data;
using SchoolDiary.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using SchoolDiary.ServicesViewModels.Courses;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolDiary.Services.Resourses.Implementation
{
    public class ResourcesService : IResourcesService
    {
        private readonly SchoolDbContext _dbContext;
        private readonly ICourseService _courseService;

        public ResourcesService(SchoolDbContext dbContext,
             ICourseService courseService)
        {
            this._dbContext = dbContext;
            this._courseService = courseService;
        }

        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public void UploadResource(CourseDetailsViewModel model)
        {
            

            foreach (var formFile in model.Files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        formFile.CopyToAsync(stream);

                        var resource = new Resource
                        {
                            Name = formFile.FileName,
                            File = stream.ToArray(),                            
                        };

                        var courseResource = new CoursesResources
                        {
                            CourseId = model.Id,
                            ResourceId = resource.Id
                        };

                        
                        this._dbContext.Resources.Add(resource);
                        this._dbContext.CoursesResources.Add(courseResource);

                        this._dbContext.SaveChanges();
                    }
                }
            }

        }

        public IEnumerable<Resource> DownloadResources(string id)
        {
            var course = this._dbContext.Courses
                .FirstOrDefault(c => c.Id == id);
            

            var resources = this._dbContext.CoursesResources
                  .Where(x => x.CourseId == id)
                  .Select(x => x.Resource)
                  .ToList();


            return resources;
                
        }
    }
}
