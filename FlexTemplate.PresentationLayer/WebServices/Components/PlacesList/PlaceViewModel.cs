﻿using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesList
{
    public class PlaceViewModel
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
