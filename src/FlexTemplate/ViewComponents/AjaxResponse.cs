using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewComponents
{
    public class AjaxResponse
    {
        public bool Successed { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
