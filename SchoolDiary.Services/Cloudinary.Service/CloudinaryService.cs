using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using SchoolDiary.Models;
using SchoolDiary.Services.Contracts;
using SchoolDiary.Services.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchoolDiary.Services.CloudinaryService
{
    public class CloudinaryService : ICloudinaryService
    {
        private const string CategoryPictureFolder = "CategoryPicture";
        private const string CoursePictureFolder = "CoursePicture";
        private const string EventPictureFolder = "EventPicture";
        private const string ArticlePictureFolder = "ArticlePicture";


        private const string CloudinaryDataConstantsCloudName = "webuni";
        private const string CloudinaryDataConstantsApiKey = "218153128111321";
        private const string CloudinaryDataConstantsApiSecret = "43KltD1bqHNdFGC7vIOqg7o6mlM";

        private readonly Dictionary<Type, string> EntityFolders = new Dictionary<Type, string>
        {
            { typeof(Category), CategoryPictureFolder },
            { typeof(Course), CoursePictureFolder},
              { typeof(Event), EventPictureFolder},
              { typeof(Article), ArticlePictureFolder}

        };

        private readonly Cloudinary cloudinary;

        public CloudinaryService()
        {
            this.cloudinary = new Cloudinary(
                new Account(
                    CloudinaryDataConstantsCloudName,
                    CloudinaryDataConstantsApiKey,
                    CloudinaryDataConstantsApiSecret));
        }

        public ImageUploadResult UploadPicture(Type entityType, string pictureId, Stream fileStream)
        {
            if (entityType == null || string.IsNullOrEmpty(pictureId) || string.IsNullOrWhiteSpace(pictureId) || fileStream == null)
                return null;

            ImageUploadParams imageUploadParams = new ImageUploadParams
            {
                File = new FileDescription(pictureId, fileStream),
                Folder = this.EntityFolders[entityType],
                PublicId = pictureId
            };

            return this.cloudinary.Upload(imageUploadParams);
        }

        public string BuildCategoryPictureUrl(string categoryName, string imageVersion)
        {
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, CategoryPictureFolder, string.Format(GlobalConstants.CategoryPicture, categoryName));
            var pictureUrl = cloudinary.Api.UrlImgUp
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildCoursePictureUrl(string courseName, string imageVersion)
        {
            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, CoursePictureFolder, string.Format(GlobalConstants.CoursePicture, courseName));
            var pictureUrl = cloudinary.Api.UrlImgUp
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildEventPictureUrl(string eventName, string imageVersion)
        {
            if (string.IsNullOrEmpty(eventName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(eventName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, EventPictureFolder, string.Format(GlobalConstants.EventPicture, eventName));
            var pictureUrl = cloudinary.Api.UrlImgUp
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }

        public string BuildArticlePictureUrl(string articleName, string imageVersion)
        {
            if (string.IsNullOrEmpty(articleName) || string.IsNullOrEmpty(imageVersion) || string.IsNullOrWhiteSpace(articleName) || string.IsNullOrWhiteSpace(imageVersion))
                return null;

            string path = string.Format(GlobalConstants.FilePath, ArticlePictureFolder, string.Format(GlobalConstants.ArticlePicture, articleName));
            var pictureUrl = cloudinary.Api.UrlImgUp
                                    .Version(imageVersion).BuildUrl(path);
            return pictureUrl;
        }
    }
}
