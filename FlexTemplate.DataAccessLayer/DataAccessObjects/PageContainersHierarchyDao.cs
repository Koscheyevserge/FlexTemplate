using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PageContainersHierarchyDao
    {
        public IEnumerable<PageContainerElementDao> Containers { get; set; }
    }
}
