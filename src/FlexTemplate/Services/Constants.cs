using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Services
{
    /// <summary>
    /// Класс константных значений
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Название ключа куки в которой хранится UserKey
        /// </summary>
        public const string USER_KEY_COOKIE_NAME = "user_key";
        /// <summary>
        /// Продолжительность жизни куки (20 минут)
        /// </summary>
        public const long COOKIE_LIFETIME_MILLISECONDS = 1200000;
    }
}
