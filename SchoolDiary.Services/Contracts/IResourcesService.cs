using SchoolDiary.Models;
using SchoolDiary.ServicesViewModels.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolDiary.Services.Contracts
{
    public interface IResourcesService
    {
        void UploadResource(CourseDetailsViewModel model);

        IEnumerable<Resource> DownloadResources(string id);
    }
}
