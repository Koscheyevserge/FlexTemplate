using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Заведение
    /// </summary>
    public class Place : IViewable, IAuthorfull, IAuditable, ICommentable<PlaceReview>, 
        IModerateable, IPhotofull<PlaceHeaderPhoto>, IAliasfull<PlaceAlias>
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
        public int ViewsCount { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<PlaceReview> Comments { get; set; }
        public bool IsModerated { get; set; }
        public List<PlaceAlias> Aliases { get; set; }
        public Guid BlobKey { get; set; }
        
        public virtual List<PlaceHeaderPhoto> Photos { get; set; }
        public virtual List<PlaceBannerPhoto> Banners { get; set; }
        public virtual List<PlaceGalleryPhoto> Gallery { get; set; }
    }
}
