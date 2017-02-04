using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    /// Модель, что передается в компонент OtherCitiesPlaces
    /// </summary>
    public class OtherCitiesPlacesViewModel : EditableViewModel
    {
        /// <summary>
        /// Коллекция моделей, что передаются в компоненты OtherCityPlaces
        /// </summary>
        [MaxLength(4)]
        public List<OtherCityPlacesViewModel> OtherCityPlacesViewModels { get; set; }

        public OtherCitiesPlacesViewModel()
        {
            OtherCityPlacesViewModels = new List<OtherCityPlacesViewModel>
            {
                new OtherCityPlacesViewModel
                {
                    CityName = "Київ",
                    PlacesCount = 121,
                    PlaceDescriptor = "ресторан",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Львів",
                    PlacesCount = 198,
                    PlaceDescriptor = "ресторанів",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Дніпро",
                    PlacesCount = 89,
                    PlaceDescriptor = "ресторанів",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Одеса",
                    PlacesCount = 13,
                    PlaceDescriptor = "ресторанів",
                    PhotoPath = "images/hot-item/01.jpg"
                }
            };
        }
    }
}
