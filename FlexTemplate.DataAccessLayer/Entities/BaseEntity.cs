using System;
using System.ComponentModel.DataAnnotations;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Базовый класс для сущностей
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// Время модификации
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
