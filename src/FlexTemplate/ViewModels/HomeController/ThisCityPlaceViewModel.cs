using System.Collections.Generic;

namespace FlexTemplate.ViewModels.HomeController
{
    public class ThisCityPlaceViewModel: EditableViewModel
    {
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
        public List<string> Categories { get; set; }      
    }
}
