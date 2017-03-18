using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class PageContainerTemplate : BaseEntity
    {
        /// <summary>
        /// Позиция компонента на странице - компоненты строятся по очереди в порядке возрастания
        /// </summary>
        public int Position { get; set; }
        public int ContainerTemplateId { get; set; }
        public ContainerTemplate ContainerTemplate { get; set; }
    }
}
