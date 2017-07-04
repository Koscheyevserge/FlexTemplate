using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Header
{
    public class ViewModel
    {
        public IEnumerable<KeyValuePair<int, string>> Languages { get; set; }
        public string CurrentLanguageName { get; set; }
        public bool IsLogined { get; set; }
        public string UserName { get; set; }
    }
}
