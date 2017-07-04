using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.Core
{
    public abstract class FlexAuthorfullPageViewModel : FlexPageViewModel
    {
        public bool IsAuthor { get; set; }
    }
}
