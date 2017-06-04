using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class HeaderViewComponentDao
    {
        public IEnumerable<KeyValuePair<int, string>> Languages { get; set; }
        public string CurrentLanguageName { get; set; }
        public bool IsLogined { get; set; }
        public string UserName { get; set; }
        public string TemplateName { get; set; }
    }
}
