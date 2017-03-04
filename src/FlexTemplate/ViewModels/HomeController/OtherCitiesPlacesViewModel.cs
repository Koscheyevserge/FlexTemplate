using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlexTemplate.ViewModels.HomeController
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
        public List<int> OtherCitiesPlacesIds { get; set; }
        public Dictionary<string,string> Strings { get; set; } 
    }
}
