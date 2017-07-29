using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        [Key]
        int Id { get; set; }
    }
}
