using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlexTemplate.Services
{
    /// <summary>
    /// Класс константных значений
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Название ключа куки в которой хранится выбраный язык пользователя
        /// </summary>
        public const string LANGUAGE_COOKIE_NAME = "LANGUAGE_COOKIE";

        public const string CURRENT_PLACE_COOKIE_NAME = "CURRENT_PLACE_COOKIE";
        /// <summary>
        /// Продолжительность жизни куки (20 минут)
        /// </summary>
        public const long COOKIE_LIFETIME_MILLISECONDS = 1200000;
    }

    public static class Extentions
    {
        public static string StripHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
