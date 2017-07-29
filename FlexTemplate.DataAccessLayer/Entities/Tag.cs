using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Tag : IAliasfull<TagAlias>
    {
        public string Name { get; set; }
        public virtual List<BlogTag> BlogTags { get; set; }
        public int Id { get; set; }
        public virtual List<TagAlias> Aliases { get; set; }
    }
}
