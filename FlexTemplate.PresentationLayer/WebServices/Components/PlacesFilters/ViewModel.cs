using System.Collections.Generic;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesFilters
{
    public class ViewModel
    {
        public IEnumerable<CategoryViewModel> AllCategories { get; set; }
        public IEnumerable<CityViewModel> AllCities { get; set; }
        public IEnumerable<int> Categories { get; set; }
        public int ListType { get; set; }
        public IEnumerable<int> Cities { get; set; }
        public string Input { get; set; }
        public bool IsDescending { get; set; }
        public int OrderBy { get; set; }
    }
}
