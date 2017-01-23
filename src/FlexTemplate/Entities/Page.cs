using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Расширения для страницы
    /// </summary>
    public class Page : BaseEntity
    {
        /// <summary>
        /// Название страницы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Локализируемые строки
        /// </summary>
        public List<PageLocalizableString> LocalizableStrings { get; set; }
        /// <summary>
        /// Фотографии
        /// </summary>
        public List<PagePhoto> Photos { get; set; } 
    }
}
