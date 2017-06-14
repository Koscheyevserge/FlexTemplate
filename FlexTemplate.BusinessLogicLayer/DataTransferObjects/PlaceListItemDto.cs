using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceListItemDto
    {
        public int Id { get; set; }
        public string HeadPhoto { get; set; }
        public string Name { get; set; }
        public int ReviewsCount { get; set; }
        public string Address { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
        public string Description { get; set; }
        public double AveragePrice { get; set; }
        public int Stars { get; set; }
    }
}
