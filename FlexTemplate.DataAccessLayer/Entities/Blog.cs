using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Blog : IViewable, IAuditable, IAuthorfull, IPhotofull<BlogPhoto>, ICommentable<BlogComment>
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public virtual List<BlogComment> Comments { get; set; }
        public string Text { get; set; }
        public bool IsModerated { get; set; }
        public virtual List<BlogTag> BlogTags { get; set; }
        public virtual List<BlogBlogCategory> BlogBlogCategories { get; set; }
        public Guid BlobKey { get; set; }
        public virtual List<BlogPhoto> Photos { get; set; }
        public int ViewsCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public User User { get; set; }
    }
}
