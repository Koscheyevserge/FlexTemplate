using Microsoft.AspNetCore.Http;

namespace FlexTemplate.PresentationLayer.Core
{
    /// <summary>
    /// Класс для работы с куки
    /// </summary>
    public static class CookieProvider
    {
        public static void AppendLanguage(HttpContext httpContext, string languageShortName)
        {
            httpContext.Response.Cookies.Append(Constants.LANGUAGE_COOKIE_NAME, languageShortName);
        }

        public static string GetLanguage(HttpContext httpContext)
        {
            return httpContext.Request.Cookies.TryGetValue(Constants.LANGUAGE_COOKIE_NAME, out string cookieValue) ? cookieValue : null;
        }

        public static void DeleteLanguage(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(Constants.LANGUAGE_COOKIE_NAME);
        }
    }
}
