using CloudinaryDotNet.Actions;
using System;
using System.IO;

namespace SchoolDiary.Services.Contracts
{
    public interface ICloudinaryService
    {
        ImageUploadResult UploadPicture(Type entityType, string pictureId, Stream fileStream);

        string BuildCategoryPictureUrl(string username, string imageVersion);

        string BuildCoursePictureUrl(string username, string imageVersion);

        string BuildEventPictureUrl(string username, string imageVersion);

        string BuildArticlePictureUrl(string username, string imageVersion);
    }
}
