namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Альтернативные названия заведения
    /// </summary>
    public class PlaceAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор заведения
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// Заведение
        /// </summary>
        public virtual Place Place { get; set; }
    }
}
