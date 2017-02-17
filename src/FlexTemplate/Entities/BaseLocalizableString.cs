using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    public  abstract class BaseLocalizableString : BaseAlias
    {
        /// <summary>
        /// Идентификатор элемента на странице, текст которого хранит локализируемая строка
        /// </summary>
        public string Tag { get; set; }
    }
}
