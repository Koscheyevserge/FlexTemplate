using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceMenu
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
