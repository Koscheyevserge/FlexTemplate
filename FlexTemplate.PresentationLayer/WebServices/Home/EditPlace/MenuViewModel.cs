using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.EditPlace
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
