using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.HomeController
{
    public class LoadMorePlacesViewModel
    {
        public IEnumerable<int> LoadedPlacesIds { get; set; }
    }
}
