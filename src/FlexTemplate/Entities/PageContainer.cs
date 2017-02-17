using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class PageContainer : BaseEntity
    {
        /// <summary>
        /// Позиция компонента на странице - компоненты строятся по очереди в порядке возрастания
        /// </summary>
        public int Position { get; set; }
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
    }
}
