using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class TagAlias : BaseAlias
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
