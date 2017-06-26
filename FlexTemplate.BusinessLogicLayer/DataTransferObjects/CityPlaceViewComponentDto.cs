using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CityPlaceViewComponentDto
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
