using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас города
    /// </summary>
    public class CityAlias : IAlias
    {
        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public City City { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
