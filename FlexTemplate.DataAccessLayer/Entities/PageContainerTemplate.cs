using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PageContainerTemplate : BaseEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerTemplateId { get; set; }
        public ContainerTemplate ContainerTemplate { get; set; }
        public int ParentId { get; set; }
        public int Position { get; set; }
    }
}
