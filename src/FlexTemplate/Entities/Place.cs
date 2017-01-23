using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Заведение
    /// </summary>
    public class Place : BaseEntity
    {
        /// <summary>
        /// Идентификатор улицы, на которой расположено заведение
        /// </summary>
        public int StreetId { get; set; }
        /// <summary>
        /// Улица, на которой расположено заведение
        /// </summary>
        public virtual Street Street { get; set; }
        /// <summary>
        /// Категория заведения
        /// </summary>
        public virtual List<PlaceCategory> PlaceCategories { get; set; }
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<PlaceAlias> PlaceAliases { get; set; } 
        /// <summary>
        /// Фотография заведения
        /// </summary>
        public virtual List<PlacePhoto> PlacePhotos { get; set; } 
    }
}
