using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.Services
{
    public static class CommonUtilities
    {
        public static Dictionary<string, string> LocalizableStringsToDictionary(IEnumerable<BaseLocalizableString> localizableStrings)
        {
            var result = new Dictionary<string, string>();
            foreach (var localizableString in localizableStrings)
            {
                result[localizableString.Tag] = localizableString.Text;
            }
            return result;
        }
    }
}
