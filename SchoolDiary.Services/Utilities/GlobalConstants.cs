namespace SchoolDiary.Services.Utilities
{
    public class GlobalConstants
    {
        public class UserRoles
        {
            public const string ADMINISTRATOR_ROLE = "Admin";
            public const string ROLE_STUDENT = "Student";
        }

        public class Areas
        {
            public const string Administration = "Administration";
            public const string Categories = "Categories";
            public const string Events = "Events";
            public const string Articles = "Articles";
            public const string Courses = "Courses";
        }

        public class FileExtensions
        {
            public const string JpgFormat = "jpg";

            public const string JpegFormat = "jpeg";

            public const string PngFormat = "png";

            public const string txtFormat = ".txt";

            public const string pdfFormat = ".pdf";

            public const string pptxFormat = ".pptx";

            public const string pubFormat = ".pub";

            public const string accdbFormat = ".accdb";

            public const string docxFormat = ".docx";
        }

        public const string Error = "Error";

        public const string WrongFileType = "Image file should be jpg/png/JpegFormat";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const string InvalidId = "Invalid Id";

        public const string InvalidData = "Invalid data";

        public const string InvalidLogin = "Invalid Login.";

        public const string UserExist = "User with this name already exist in this course.";

        public const string WrongFileExtensions = "Wrong File Extension";

        // Cloudinary
        public const string CategoryPicture = "{0}_categoryPicture";
        public const string CoursePicture = "{0}_coursePicture";
        public const string EventPicture = "{0}_eventPicture";
        public const string ArticlePicture = "{0}_ArticlePicture";


        public const string FilePath = "/{0}/{1}";

        public const string ProfilePicture = "{0}_categoryPicture";
    }
}
