using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Services
{
    public static class LocalizableStringsProvider
    {
        public static Dictionary<string, string> GetStrings(Context context, string containerName, bool isAdmin = false)
        {
            var localizableStringReplaceRegex = new Regex("dataId=[\"']\\d*[\"']");
            var isEditableRegex = new Regex("contenteditable=[\"']true[\"']");
            return context.Containers.Include(c => c.LocalizableStrings)
                .FirstOrDefault(c => c.Name == containerName)?
                .LocalizableStrings.ToDictionary(ls => ls.Tag, ls => 
                isEditableRegex.Replace(localizableStringReplaceRegex.Replace(ls.Text, isAdmin ? $"dataId=\"{ls.Id}\"" : ""), isAdmin ? "contenteditable=\"true\"" : ""));
        }
    }
}
