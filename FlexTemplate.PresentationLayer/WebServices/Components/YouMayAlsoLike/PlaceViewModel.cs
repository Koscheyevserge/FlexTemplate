using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.YouMayAlsoLike
{
    public class PlaceViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public int ReviewsCount { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int Id { get; set; }
    }
}
