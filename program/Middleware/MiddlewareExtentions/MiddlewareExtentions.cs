using Microsoft.AspNetCore.Builder;

namespace SchoolDiary.Web.Middleware.MiddlewareExtentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseSeedDataMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedDataMiddleware>();
        }
    }
}
