using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас страны
    /// </summary>
    public class CountryAlias : IAlias
    {
        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
