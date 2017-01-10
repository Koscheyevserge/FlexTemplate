namespace FlexTemplate.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Категория заведения
    /// </summary>
    public class PlaceCategory : BaseEntity
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
        public int CategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public virtual Category Category { get; set; }
    }
}
