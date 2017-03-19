using System.Collections.Generic;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class ThisCityPlaceViewModel: EditableViewModel
    {
        public int PlaceId { get; set; }
        /// <summary>
        /// Фото, что будет отображаться в компоненте
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// Название места
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Оценка
        /// </summary>
        public double Stars { get; set; }
        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewsCount { get; set; }
        /// <summary>
        /// Коллекция моделей, что передаются в компонент ThisCityPlace
        /// </summary>
        public IEnumerable<Category> Categories { get; set; }      
    }
}
