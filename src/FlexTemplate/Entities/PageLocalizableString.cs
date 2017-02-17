using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Локализируемая строка страницы
    /// </summary>
    public class PageLocalizableString : BaseLocalizableString
    {
        /// <summary>
        /// Идентификатор страницы
        /// </summary>
        public int PageId { get; set; }
        /// <summary>
        /// Страница
        /// </summary>
        public Page Page { get; set; }
    }
}
