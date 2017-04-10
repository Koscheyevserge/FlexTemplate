using System;
using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    public class Blog : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public string Caption { get; set; }
        public User Author { get; set; }
        public virtual List<BlogComment> Comments { get; set; }
        public string Preamble { get; set; }
        public string Text { get; set; }
    }
}
