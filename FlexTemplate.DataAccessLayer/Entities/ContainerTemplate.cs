using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class ContainerTemplate : IEntity
    {
        public int ContainerId { get; set; }

        public Container Container { get; set; }

        /// <summary>
        /// Имя шаблона компонента
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Развязочная таблица компонентов на странице
        /// </summary>
        public virtual List<PageContainerTemplate> PageContainerTemplates { get; set; }
        public int Id { get; set; }
    }
}
