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
        public static void AppendLanguage(HttpContext httpContext, Context _context, Language language)
        {
            httpContext.Response.Cookies.Append(Constants.LANGUAGE_COOKIE_NAME, language.ShortName, new CookieOptions {Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(Constants.COOKIE_LIFETIME_MILLISECONDS))});
        }

        public static Language GetLanguage(HttpContext httpContext, Context _context)
        {
            string cookieValue;
            return httpContext.Request.Cookies.TryGetValue(Constants.LANGUAGE_COOKIE_NAME, out cookieValue) ? _context.Languages.FirstOrDefault(uc => uc.ShortName == cookieValue) : null;
        }

        public static void DeleteLanguage(HttpContext httpContext, Context _context)
        {
            httpContext.Response.Cookies.Delete(Constants.LANGUAGE_COOKIE_NAME);
        }
    }
}
