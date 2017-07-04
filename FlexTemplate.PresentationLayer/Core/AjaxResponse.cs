using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.Core
{
    public class AjaxResponse
    {
        public bool Successed { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
