using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PageContainerElementDto
    {
        public int Id { get; set; }
        public string ContainerName { get; set; }
        public string ContainerTemplateName { get; set; }
        public IEnumerable<PageContainerElementDto> NestedContainers { get; set; }
        public PageContainerElementDto ParentContainer { get; set; }
        public int Position { get; set; }
    }
}
