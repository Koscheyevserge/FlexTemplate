using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Заведение
    /// </summary>
    public class Place : BaseEntity
    {
        /// <summary>
        /// Название заведения
        /// </summary>
        public string Name { get; set; }
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
        public virtual List<PlaceAlias> Aliases { get; set; } 
        /// <summary>
        /// Фотография заведения
        /// </summary>
        public virtual List<PlacePhoto> Photos { get; set; }
        /// <summary>
        /// Отзывы заведения
        /// </summary>
        public virtual List<PlaceReview> Reviews { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public string HouseText { get; set; }
    }
}
