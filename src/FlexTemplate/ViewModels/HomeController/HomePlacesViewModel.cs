using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.HomeController
{
    public class HomePlacesViewModel
    {
        public int[] Cities { get; set; }
        public int[] Categories { get; set; }
        public string Input { get; set; }
        public string ListType { get; set; }
        public int CurrentPage { get; set; }
    }
}
