using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesPagination
{
    public class ViewModel
    {
        public bool HasPreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public int Pages { get; set; }
        public int ListType { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public IEnumerable<int> Cities { get; set; }
        public string Input { get; set; }
        public bool IsDescending { get; set; }
        public int OrderBy { get; set; }
    }
}
