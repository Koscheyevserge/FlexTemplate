using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Фотография на странице
    /// </summary>
    public class ContainerPhoto : BasePhoto
    {
        /// <summary>
        /// Идентификатор компонента
        /// </summary>
        public int ContainerId { get; set; }
        /// <summary>
        /// Компонент
        /// </summary>
        public Container Container { get; set; }
    }
}
