using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.HomeController
{
    public class NewPlaceProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Guid { get; set; }
        public double Price { get; set; }
    }
}
