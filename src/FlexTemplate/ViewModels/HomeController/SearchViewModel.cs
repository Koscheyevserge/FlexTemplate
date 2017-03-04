using System.Collections.Generic;

namespace FlexTemplate.ViewModels.HomeController
{
    public class SearchViewModel : EditableViewModel
    {
        public List<string> Images { get; set; }
        public List<string> CategoriesNames { get; set; }
        public Dictionary<string, string> Strings { get; set; }
    }
}
