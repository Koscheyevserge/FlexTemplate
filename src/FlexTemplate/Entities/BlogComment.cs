using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class BlogComment : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public User Author { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }
    }
}
