using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country : BaseEntity
    {
        /// <summary>
        /// Название страны
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Города
        /// </summary>
        public virtual List<City> Cities { get; set; } 
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<CountryAlias> CountryAliases { get; set; } 
    }
}
