using System.Collections.Generic;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesFilters
{
    public class PlacesFilters : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public PlacesFilters(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync([FromQuery] string input, [FromQuery] int orderBy, [FromQuery] int[] cities, [FromQuery] int[] categories, [FromQuery] bool isDescending, [FromQuery] int listType)
        {
            var allCategories = await ComponentsServices.GetPlaceCategoriesChecklistItems(HttpContext.User, categories ?? new int[] { });
            var allCities = await ComponentsServices.GetCityChecklistItems(HttpContext.User, cities ?? new int[] { });
            var model = new ViewModel
            {
                Input = input,
                OrderBy = orderBy,
                Cities = cities,
                ListType = listType,
                IsDescending = isDescending,
                Categories = categories,
                AllCategories = allCategories.To<IEnumerable<CategoryViewModel>>(),
                AllCities = allCities.To<IEnumerable<CityViewModel>>()
            };
            return View(model);
        }
    }
}
