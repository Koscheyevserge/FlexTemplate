using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас улицы
    /// </summary>
    public class StreetAlias : IAlias
    {
        /// <summary>
        /// Идентификатор улицы
        /// </summary>
        public int StreetId { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public Street Street { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
