using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Container : IEntity
    {
        /// <summary>
        /// Имя компонента
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
        public int Id { get; set; }
    }
}