using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Load.LoadMorePlaces
{
    public class PostModel
    {
        public IEnumerable<int> LoadedPlacesIds { get; set; }
    }
}
