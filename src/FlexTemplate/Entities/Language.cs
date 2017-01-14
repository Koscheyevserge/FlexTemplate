﻿using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Язык локализации
    /// </summary>
    public class Language : BaseEntity
    {
        /// <summary>
        /// Название языка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Аббревиатура названия языка
        /// </summary>
        [MaxLength(2)]
        public string ShortName { get; set; }
        /// <summary>
        /// Локализируемые строки
        /// </summary>
        public virtual List<LocalizableString> LocalizableStrings { get; set; } 
    }
}
