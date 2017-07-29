using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Улица
    /// </summary>
    public class Street : IAliasfull<StreetAlias>
    {
        /// <summary>
        /// Название улицы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<StreetAlias> Aliases { get; set; } 
        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public virtual City City { get; set; }
        public int Id { get; set; }
    }
}
