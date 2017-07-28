using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PageContainerElementDao
    {
        public int Id { get; set; }
        public string ContainerName { get; set; }
        public string ContainerTemplateName { get; set; }
        public IEnumerable<PageContainerElementDao> NestedContainers { get; set; }
        public int ParentId { get; set; }
        public PageContainerElementDao ParentContainer { get; set; }
        public int Position { get; set; }
    }
}
