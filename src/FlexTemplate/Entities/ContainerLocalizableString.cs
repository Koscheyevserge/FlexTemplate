using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public class ContainerLocalizableString : BaseLocalizableString
    {
        /// <summary>
        /// Идентификатор вьюкомпонента
        /// </summary>
        public int ContainerId { get; set; }
        /// <summary>
        /// Вьюкомпонент
        /// </summary>
        public Container Container { get; set; }
    }
}
