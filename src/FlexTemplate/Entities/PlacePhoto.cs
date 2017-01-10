namespace FlexTemplate.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Фотографии заведения
    /// </summary>
    public class PlacePhoto : BaseEntity
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
        /// Идентификатор фотографии
        /// </summary>
        public int FileId { get; set; }
        /// <summary>
        /// Фотография
        /// </summary>
        public virtual File File { get; set; }
    }
}
