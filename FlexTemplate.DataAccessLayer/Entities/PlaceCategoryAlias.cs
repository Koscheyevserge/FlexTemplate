namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас категории
    /// </summary>
    public class PlaceCategoryAlias : BaseAlias
    {
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
