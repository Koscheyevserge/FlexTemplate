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
                    CityName = "Киев",
                    PlacesCount = 121,
                    PlaceDescriptor = "заведений",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Львов",
                    PlacesCount = 198,
                    PlaceDescriptor = "заведений",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Днепр",
                    PlacesCount = 89,
                    PlaceDescriptor = "заведений",
                    PhotoPath = "images/hot-item/01.jpg"
                },
                new OtherCityPlacesViewModel
                {
                    CityName = "Одесса",
                    PlacesCount = 13,
                    PlaceDescriptor = "заведений",
                    PhotoPath = "images/hot-item/01.jpg"
                }
            };
        }
    }
}
