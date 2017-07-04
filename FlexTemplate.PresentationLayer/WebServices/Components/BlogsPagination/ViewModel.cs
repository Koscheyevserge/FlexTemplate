using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsPagination
{
    public class ViewModel
    {
        public bool HasPreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public int Pages { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Tags { get; set; }
        public string Input { get; set; }
    }
}
