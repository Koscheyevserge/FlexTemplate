using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Язык локализации
    /// </summary>
    public class Language : BaseEntity
    {
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// Название языка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аббревиатура названия языка
        /// </summary>
        [MaxLength(2)]
        public string ShortName { get; set; }
    }
}
