using System.Collections.Generic;

namespace FlexTemplate.ViewModels
{
    public class AjaxResponse
    {
        public bool Successed { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
