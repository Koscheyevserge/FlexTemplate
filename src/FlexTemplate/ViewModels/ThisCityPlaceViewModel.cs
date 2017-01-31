using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
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
        public float Stars { get; set; }
        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewsCount { get; set; }
        /// <summary>
        /// Коллекция моделей, что передаются в компонент ThisCityPlace
        /// </summary>
        public virtual List<ThisCityPlaceViewModel> Categories { get; set; }      
    }
}
