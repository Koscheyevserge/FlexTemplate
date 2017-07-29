using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас категории
    /// </summary>
    public class PlaceCategoryAlias : IAlias
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int PlaceCategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public PlaceCategory PlaceCategory { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
