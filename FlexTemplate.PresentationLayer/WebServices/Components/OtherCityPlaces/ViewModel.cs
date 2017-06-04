using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCityPlaces
{
    public class ViewModel
    {
        public int PlacesCount { get; set; }
        public string PlaceDescriptor { get; set; }
        public string CityName { get; set; }
        public string PhotoPath { get; set; }
        public int CityId { get; set; }
    }
}
