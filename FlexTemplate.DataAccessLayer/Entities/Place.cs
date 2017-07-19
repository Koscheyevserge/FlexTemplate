using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Заведение
    /// </summary>
    public class Place : BaseAuthorfullViewableEntity
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
        public virtual List<PlacePlaceCategory> PlacePlaceCategories { get; set; }
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
        public virtual List<PlaceCommunication> Communications { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        /// <summary>
        /// Расписание заведения
        /// </summary>
        public PlaceSchedule Schedule { get; set; }
        /// <summary>
        /// Расписание заведения
        /// </summary>
        public virtual List<Menu> Menus { get; set; }
        /// <summary>
        /// Фичи заведения
        /// </summary>
        public virtual List<PlaceFeatureColumn> FeatureColumns { get; set; }

        public virtual List<PlaceHeaderPhoto> Headers { get; set; }

        public virtual List<PlaceGalleryPhoto> Gallery { get; set; }

        public virtual List<PlaceBannerPhoto> Banners { get; set; }
    }
}
