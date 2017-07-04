using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<BlogBlogCategory> BlogBlogCategories { get; set; }
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<BlogCategoryAlias> Aliases { get; set; }
    }
}
