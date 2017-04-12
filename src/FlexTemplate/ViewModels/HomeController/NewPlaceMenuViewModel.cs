using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.HomeController
{
    public class NewPlaceMenuViewModel
    {
        public string Name { get; set; }
        public NewPlaceProductViewModel[] Products { get; set; }
    }
}
