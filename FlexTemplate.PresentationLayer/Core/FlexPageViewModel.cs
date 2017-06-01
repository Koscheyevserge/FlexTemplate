using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;

namespace FlexTemplate.PresentationLayer.Core
{
    public abstract class FlexPageViewModel
    {
        public PageContainersHierarchyDto Hierarchy { get; set; }
        public bool CanEditVisuals { get; set; }
    }
}
