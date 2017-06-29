using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class TagViewModel
    {
        public string Name { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
