using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class ThisPlacesFiltersViewModel
    {
        public IEnumerable<City> Cities { get; set; }
        public IEnumerable<Category> Categories { get; set; } 
        public IEnumerable<int> SelectedCities { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; } 
    }
}
