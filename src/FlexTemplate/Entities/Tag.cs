using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    public class Tag : BaseEntity
    {
        /// <summary>
        /// Название тэга
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Категория заведения
        /// </summary>
        public virtual List<BlogTag> BlogTags { get; set; }
    }
}
