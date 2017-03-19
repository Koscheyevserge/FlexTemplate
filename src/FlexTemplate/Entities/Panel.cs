using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class Panel : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Container> Containers { get; set; } 
    }
}
