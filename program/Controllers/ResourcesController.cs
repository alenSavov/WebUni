using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolDiary.Data;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using SchoolDiary.ServicesViewModels.Courses;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SchoolDiary.Web.Controllers
{
    [Authorize]
    public class ResourcesController : Controller
    {
        private const string FileNotSelected = "File not selected";
        private const string FileUploadMessage = "Successfully uploaded file";

        private const string CourdeDetailsPath = "/Courses/Courses/Details?id=";
        
        private readonly IResourcesService _resourcesService;

        public ResourcesController(IResourcesService resourcesService)
        {
            this._resourcesService = resourcesService;
        }

        [Authorize(Roles = GlobalConstants.UserRoles.ADMINISTRATOR_ROLE)]
        public IActionResult Upload(CourseDetailsViewModel model)
        {
            if (model.Files == null)
            {
                TempData[GlobalConstants.TempDataErrorMessageKey] = FileNotSelected;

                return Redirect(CourdeDetailsPath + model.Id);
            }

            var fileExtensions = new string[] { GlobalConstants.FileExtensions.txtFormat,
                                                GlobalConstants.FileExtensions.pptxFormat,
                                                GlobalConstants.FileExtensions.pubFormat,
                                                GlobalConstants.FileExtensions.accdbFormat,
                                                GlobalConstants.FileExtensions.docxFormat,
                                                GlobalConstants.FileExtensions.pdfFormat
            };

            foreach (var file in model.Files)
            {
                string ext = Path.GetExtension(file.FileName);
                if (!fileExtensions.Contains(ext))
                {
                    TempData[GlobalConstants.TempDataErrorMessageKey] = $"File: {file.FileName} {GlobalConstants.WrongFileExtensions}";
                    return Redirect("/");
                }
            }

            this._resourcesService.UploadResource(model);
            TempData[GlobalConstants.TempDataSuccessMessageKey] = FileUploadMessage;

            return Redirect("/");
        }

        [Authorize]
        public FileResult DownloadResources(string id)
        {
            var resources = this._resourcesService.DownloadResources(id)
                .ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                using (var archieve = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    foreach (var resource in resources)
                    {
                        var fileName = resource.Name;
                        var ext = Path.GetExtension(fileName);

                        var entry = archieve.CreateEntry(fileName + ext, CompressionLevel.Fastest);

                        using (var zipStream = entry.Open())
                        {
                            zipStream.Write(resource.File, 0, resource.File.Length);
                        }
                    }
                }
                return File(ms.ToArray(), "application/zip", "Archive.zip");
            }

        }
    }
}
