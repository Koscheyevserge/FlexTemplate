using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BasePhoto : BaseEntity
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string Uri { get; set; }
        /// <summary>
        /// Активен
        /// </summary>
        public bool IsActive { get; set; }
    }
}
