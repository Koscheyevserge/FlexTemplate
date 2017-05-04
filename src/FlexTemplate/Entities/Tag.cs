using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<BlogTag> BlogTags { get; set; }
        public virtual List<TagAlias> TagAliases { get; set; }
    }
}
