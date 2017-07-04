using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Breadcrumbs
{
    public class BreadcrumbViewModel
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
