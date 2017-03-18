using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class AvailableContainer : BaseEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
    }
}
