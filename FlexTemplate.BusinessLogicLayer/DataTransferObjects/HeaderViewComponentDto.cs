using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class HeaderViewComponentDto
    {
        public IEnumerable<KeyValuePair<int, string>> Languages { get; set; }
        public string CurrentLanguageName { get; set; }
        public bool IsLogined { get; set; }
        public string UserName { get; set; }
        public string TemplateName { get; set; }
    }
}
