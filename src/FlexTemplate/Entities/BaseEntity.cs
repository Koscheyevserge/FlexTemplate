namespace FlexTemplate.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Базовый класс для сущностей
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
