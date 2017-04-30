using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Фичи заведения
    /// </summary>
    public class PlaceFeature : BaseEntity
    {
        /// <summary>
        /// Заведение
        /// </summary>
        public Place Place { get; set; }
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
        public int Column { get; set; }
    }
}
