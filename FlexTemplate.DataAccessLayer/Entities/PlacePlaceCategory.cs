namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Категория заведения
    /// </summary>
    public class PlacePlaceCategory : BaseEntity
    {
        /// <summary>
        /// Идентификатор заведения
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// Заведение
        /// </summary>
        public virtual Place Place { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int PlaceCategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public PlaceCategory PlaceCategory { get; set; }
    }
}
