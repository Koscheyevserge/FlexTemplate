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
        public OtherCitiesPlacesViewModel OtherCitiesPlacesViewModel { get; set; }
        public ThisCityPlacesViewModel ThisCityPlacesViewModel { get; set; }

        public HomeIndexViewModel()
        {
            OtherCitiesPlacesViewModel = new OtherCitiesPlacesViewModel();
            ThisCityPlacesViewModel = new ThisCityPlacesViewModel();
        }
    }
}
