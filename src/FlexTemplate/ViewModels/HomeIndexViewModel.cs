using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    /// <summary>
    /// Модель, что передается на Home/Index
    /// </summary>
    public class HomeIndexViewModel
    {
        /// <summary>
        /// Модель, что передается в компонент OtherCitiesPlaces
        /// </summary>
        public OtherCitiesPlacesViewModel OtherCitiesPlacesViewModel { get; set; }
    }
}
