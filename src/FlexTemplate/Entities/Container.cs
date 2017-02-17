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
        /// Имя шаблона компонента (например "Default")
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Локализируемые строки
        /// </summary>
        public virtual List<ContainerLocalizableString> LocalizableStrings { get; set; }
        /// <summary>
        /// Развязочная таблица компонентов на странице
        /// </summary>
        public virtual List<PageContainer> PageContainers { get; set; }
    }
}
