using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlace
{
    public class ViewModel
    {
        public int PlaceId { get; set; }
        public string PhotoPath { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Stars { get; set; }
        public int ReviewsCount { get; set; }
        public string ReviewsDescriptor { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }
}
