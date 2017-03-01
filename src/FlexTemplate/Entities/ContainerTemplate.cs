using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class ContainerTemplate : BaseEntity
    {
        public int ContainerId { get; set; }

        public Container Container { get; set; }

        /// <summary>
        /// Имя шаблона компонента (например "LeftWideAnimated")
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Развязочная таблица компонентов на странице
        /// </summary>
        public virtual List<PageContainerTemplate> PageContainerTemplates { get; set; }
    }
}
