using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class Container : BaseEntity
    {
        /// <summary>
        /// Идентификатор страницы, на которой доступен этот контейнер
        /// </summary>
        public int PageId { get; set; }
        /// <summary>
        /// Страница, на которой доступен этот контейнер
        /// </summary>
        public Page Page { get; set; }
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
        /// <summary>
        /// Изображения
        /// </summary>
        public virtual List<ContainerPhoto> Photos { get; set; }
    }
}