using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SchoolDiary.Services.Utilities;

namespace SchoolDiary.Web.Infrastructure
{
    public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[GlobalConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[GlobalConstants.TempDataErrorMessageKey] = message;
        }

        public static void AddAdminSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[GlobalConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddAdminErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[GlobalConstants.TempDataErrorMessageKey] = message;
        }
    }
}
