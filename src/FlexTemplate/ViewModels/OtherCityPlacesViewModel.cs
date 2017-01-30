using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    /// Модель, что передается в OtherCityPlaces
    /// </summary>
    public class OtherCityPlacesViewModel
    {
        /// <summary>
        /// Фото, что будет отображаться в компоненте
        /// </summary>
        public string PhotoPath { get; set; }
        /// <summary>
        /// Название города
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// Количество заведений в городе
        /// </summary>
        public int PlacesCount { get; set; }
        /// <summary>
        /// Приставка к количеству заведений, например 432 ресторана/заведения/places и тд.
        /// </summary>
        public string PlaceDescriptor { get; set; }
    }
}
