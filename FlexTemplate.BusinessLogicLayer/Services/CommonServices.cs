using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.DataAccessLayer.Entities;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public static class CommonServices
    {
        public static async Task<int> GetPlacesPerPageCountAsync()
        {
            return 12;//TODO реализовать как системную настройку
        }

        public static async Task<int> GetBlogsPerPageCountAsync()
        {
            return 12;//TODO реализовать как системную настройку
        }

        public static IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, Language defaultLanguage, Language userLanguage) where T : BaseCachedNameDto
        {
            return GetProperAliases(aliases, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        public static IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, string name, Language defaultLanguage, Language userLanguage) where T : BaseCachedNameDto
        {
            return GetProperAliases(aliases, name, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        public static string GetProperAlias<T>(IEnumerable<T> aliases, string name, Language defaultLanguage, Language userLanguage) where T : BaseCachedNameDto
        {
            return GetProperAlias(aliases, name, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        public static string GetProperAlias<T>(IEnumerable<T> aliases, Language defaultLanguage, Language userLanguage) where T : BaseCachedNameDto
        {
            return GetProperAlias(aliases, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        public static IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, int defaultLanguageId, int userLanguageId) where T : BaseCachedNameDto
        {
            var result = new List<string>();
            var aliasesList = aliases.ToList();
            result.AddRange(aliasesList.Where(a => a.LanguageId == userLanguageId).Select(a => a.Name));
            if (!result.Any())
            {
                result.AddRange(aliasesList.Where(a => a.LanguageId == defaultLanguageId).Select(a => a.Name));
            }
            return result;
        }
        public static IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, string name, int defaultLanguageId, int userLanguageId) where T : BaseCachedNameDto
        {
            var result = GetProperAliases(aliases, defaultLanguageId, userLanguageId).ToList();
            if (!result.Any())
            {
                result.Add(name);
            }
            return result;
        }
        public static string GetProperAlias<T>(IEnumerable<T> aliases, string name, int defaultLanguageId, int userLanguageId) where T : BaseCachedNameDto
        {
            return GetProperAliases(aliases, name, defaultLanguageId, userLanguageId).FirstOrDefault();
        }
        public static string GetProperAlias<T>(IEnumerable<T> aliases, int defaultLanguageId, int userLanguageId) where T : BaseCachedNameDto
        {
            return GetProperAliases(aliases, defaultLanguageId, userLanguageId).FirstOrDefault();
        }
    }
}
