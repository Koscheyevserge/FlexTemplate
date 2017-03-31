using System.Collections.Generic;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class SearchViewModel : EditableViewModel
    {
        public List<string> Images { get; set; }
        public List<Category> Categories { get; set; }
        public List<City> Cities { get; set; }
        public Dictionary<string, string> Strings { get; set; }
    }
}
