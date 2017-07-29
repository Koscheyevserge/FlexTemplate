using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Альтернативные названия заведения
    /// </summary>
    public class PlaceAlias : IAlias
    {
        /// <summary>
        /// Идентификатор заведения
        /// </summary>
        public int PlaceId { get; set; }
        /// <summary>
        /// Заведение
        /// </summary>
        public virtual Place Place { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
