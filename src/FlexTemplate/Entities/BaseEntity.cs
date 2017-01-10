using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FlexTemplate.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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
