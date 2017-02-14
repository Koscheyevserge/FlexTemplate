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
    public class CookieProvider
    {
        private readonly Context _context;
        private readonly HttpContext _httpContext;

        public CookieProvider(HttpContext httpContext, Context context)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public void AppendLanguage(Language language)
        {
            _httpContext.Response.Cookies.Append(Constants.LANGUAGE_COOKIE_NAME, language.ShortName, new CookieOptions {Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(Constants.COOKIE_LIFETIME_MILLISECONDS))});
        }

        public Language GetLanguage()
        {
            string cookieValue;
            return _httpContext.Request.Cookies.TryGetValue(Constants.LANGUAGE_COOKIE_NAME, out cookieValue) ? _context.Languages.FirstOrDefault(uc => uc.ShortName == cookieValue) : null;
        }

        public void DeleteLanguage()
        {
            _httpContext.Response.Cookies.Delete(Constants.LANGUAGE_COOKIE_NAME);
        }
    }
}
