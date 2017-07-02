using System.Collections.Generic;
using System.Linq;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var input = Request.Query["input"].ToString();
            int.TryParse(Request.Query["orderBy"], out int orderBy);
            var cities = Request.Query["cities"].ToArray().Select(int.Parse);
            var categories = Request.Query["categories"].ToArray().Select(int.Parse);
            bool.TryParse(Request.Query["isDescending"], out bool isDescending);
            int.TryParse(Request.Query["listType"], out int listType);
            var allCategories = await ComponentsServices.GetPlaceCategoriesChecklistItems(HttpContext.User, categories);
            var allCities = await ComponentsServices.GetCityChecklistItems(HttpContext.User, cities);
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
