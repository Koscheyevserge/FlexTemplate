using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels
{
    public class ThisCityPlaceViewModel
    {
        public string PhotoPath { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public float Stars { get; set; }
        public int ReviewsCount { get; set; }
        public virtual List<ThisCityPlaceViewModel> Categories { get; set; }



      
    }
}
