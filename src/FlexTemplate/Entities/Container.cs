using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class Container : BaseEntity
    {
        /// <summary>
        /// Имя компонента (например "Search")
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Локализируемые строки
        /// </summary>
        public virtual List<ContainerLocalizableString> LocalizableStrings { get; set; }
        /// <summary>
        /// Шаблоны компонента
        /// </summary>
        public virtual List<ContainerTemplate> ContainerTemplates { get; set; } 
    }
}
