using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Фичи заведения
    /// </summary>
    public class PlaceFeature : IEntity
    {
        /// <summary>
        /// Название фичи
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Строка для размещения фичи
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// Колонка для размещения фичи
        /// </summary>
        public int PlaceFeatureColumnId { get; set; }
        public PlaceFeatureColumn Column { get; set; }
        public int Id { get; set; }
    }
}
