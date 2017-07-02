using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class CategoryViewModel
    {
        public string Caption { get; set; }
        public int BlogsCount { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
