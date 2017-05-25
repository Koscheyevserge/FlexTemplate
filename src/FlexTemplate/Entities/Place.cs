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
        public string Description { get; set; }
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
        /// Отзывы заведения
        /// </summary>
        public virtual List<PlaceReview> Reviews { get; set; }
        /// <summary>
        /// Номер дома
        /// </summary>
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// Расписание заведения
        /// </summary>
        public Schedule Schedule { get; set; }
        /// <summary>
        /// Расписание заведения
        /// </summary>
        public virtual List<Menu> Menus { get; set; }
        /// <summary>
        /// Фичи заведения
        /// </summary>
        public virtual List<PlaceFeature> PlaceFeatures { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
