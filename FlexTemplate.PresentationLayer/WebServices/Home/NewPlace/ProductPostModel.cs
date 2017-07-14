using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.NewPlace
{
    public class ProductPostModel
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}
