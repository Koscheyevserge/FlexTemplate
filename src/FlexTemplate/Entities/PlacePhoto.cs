namespace FlexTemplate.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Фотографии заведения
    /// </summary>
    public class PlacePhoto : BasePhoto
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
