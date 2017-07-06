using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.NewPlace
{
    public class MenuPostModel
    {
        public string Name { get; set; }
        public IEnumerable<ProductPostModel> Products { get; set; }
    }
}
