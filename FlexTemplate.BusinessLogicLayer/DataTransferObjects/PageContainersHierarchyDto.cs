using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PageContainersHierarchyDto
    {
        public IEnumerable<PageContainerElementDto> Containers { get; set; }
    }
}
