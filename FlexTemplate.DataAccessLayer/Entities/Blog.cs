using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Blog : BaseAuthorfullEntity
    {
        public string Caption { get; set; }
        public virtual List<BlogComment> Comments { get; set; }
        public string Text { get; set; }
        public bool IsModerated { get; set; }
        public virtual List<BlogTag> BlogTags { get; set; }
        public virtual List<BlogBlogCategory> BlogBlogCategories { get; set; }
    }
}
