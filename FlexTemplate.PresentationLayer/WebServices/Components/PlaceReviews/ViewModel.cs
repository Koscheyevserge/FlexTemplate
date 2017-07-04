using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReviews
{
    public class ViewModel
    {
        public IEnumerable<int> Reviews { get; set; }
        public int PlaceId { get; set; }
    }
}
