using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Город
    /// </summary>
    public class City : BaseEntity
    {
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор страны, в которой находится город
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Страна, в которой находится город
        /// </summary>
        public virtual Country Country { get; set; }
        /// <summary>
        /// Улицы
        /// </summary>
        public virtual List<Street> Streets { get; set; } 
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<CityAlias> Aliases { get; set; } 
    }
}
