using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Services
{
    /// <summary>
    /// Класс для работы с куки
    /// </summary>
    public static class CookieProvider
    {
        public static void AppendLanguage(HttpContext httpContext, Language language)
        {
            httpContext.Response.Cookies.Append(Constants.LANGUAGE_COOKIE_NAME, language.ShortName);
        }

        public static string GetLanguage(HttpContext httpContext)
        {
            string cookieValue;
            return httpContext.Request.Cookies.TryGetValue(Constants.LANGUAGE_COOKIE_NAME, out cookieValue) ? cookieValue : null;
        }

        public static void DeleteLanguage(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(Constants.LANGUAGE_COOKIE_NAME);
        }
        public static void AppendCurrentPlace(HttpContext httpContext, int placeId)
        {
            httpContext.Response.Cookies.Append(Constants.CURRENT_PLACE_COOKIE_NAME, placeId.ToString());
        }

        public static int GetCurrentPlace(HttpContext httpContext)
        {
            string cookieValue;
            return httpContext.Request.Cookies.TryGetValue(Constants.CURRENT_PLACE_COOKIE_NAME, out cookieValue) ? int.Parse(cookieValue) : 0;
        }

        public static void DeleteCurrentPlace(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(Constants.CURRENT_PLACE_COOKIE_NAME);
        }
    }
}
