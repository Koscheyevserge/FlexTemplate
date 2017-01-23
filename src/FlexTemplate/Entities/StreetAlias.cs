using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Алиас улицы
    /// </summary>
    public class StreetAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор улицы
        /// </summary>
        public int StreetId { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public Street Street { get; set; }
    }
}
