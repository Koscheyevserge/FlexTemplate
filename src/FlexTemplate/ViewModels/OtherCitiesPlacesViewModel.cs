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
    }
}
