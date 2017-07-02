using System.Collections;
using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesList
{
    public class ViewModel
    {
        public IEnumerable<PlaceViewModel> Places { get; set; }
        public int ListType { get; set; }
    }
}
