using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Категория
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Категория заведения
        /// </summary>
        public virtual List<PlaceCategory> PlaceCategories { get; set; } 
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<CategoryAlias> Aliases { get; set; } 
    }
}
