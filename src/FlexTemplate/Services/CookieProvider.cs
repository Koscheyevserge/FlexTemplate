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
    /*public static class CookieProvider
    {
        /// <summary>
        /// Записывает UserKey в куки пользователя
        /// </summary>
        /// <param name="httpContext">HttpContext запроса</param>
        /// <param name="dbContext">Context базы данных</param>
        /// <param name="user">Пользователь</param>
        /// <returns>Сгенерированный UserKey</returns>
        public static UserKey AppendUser(HttpContext httpContext, Context dbContext, User user)
        {
            var userKey = new UserKey{ Key = Guid.NewGuid(), User = user};
            dbContext.UserKeys.Add(userKey);
            dbContext.SaveChanges();
            httpContext.Response.Cookies.Append(Constants.USER_KEY_COOKIE_NAME, userKey.Key.ToString(), new CookieOptions {Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(Constants.COOKIE_LIFETIME_MILLISECONDS))});
            return userKey;
        }
        /// <summary>
        /// Получает пользователя из куки клиента
        /// </summary>
        /// <param name="httpContext">HttpContext запроса</param>
        /// <param name="dbContext">Context базы данных</param>
        /// <returns>Возвращает найденого пользователя или null</returns>
        public static User GetUser(HttpContext httpContext, Context dbContext)
        {
            string cookieValue;
            if (httpContext.Request.Cookies.TryGetValue(Constants.USER_KEY_COOKIE_NAME, out cookieValue))
            {
                Guid userKeyGuid;
                if (Guid.TryParse(cookieValue, out userKeyGuid))
                {
                    var userKey = dbContext.UserKeys.Where(uc => uc.Key == userKeyGuid)
                        .Include(uc => uc.User)
                        .AsNoTracking().FirstOrDefault();
                    if (userKey != null)
                    {
                        return userKey.User;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Очищает ключ пользователя в куке клиента 
        /// </summary>
        public static void DeleteUser(HttpContext httpContext)
        {
            httpContext.Response.Cookies.Delete(Constants.USER_KEY_COOKIE_NAME);
        }
    }*/
}
