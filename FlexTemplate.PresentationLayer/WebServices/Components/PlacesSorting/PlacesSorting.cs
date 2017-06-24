using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Enums;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.WebServices.Components.PlacesPagination;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesSorting
{
    public class PlacesSorting : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public PlacesSorting(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public IViewComponentResult Invoke([FromQuery] int currentPage, [FromQuery] string input, [FromQuery] int[] cities, [FromQuery] int[] categories, [FromQuery] bool isDescending, int placesCount, [FromQuery] int orderBy = 1, [FromQuery] int listType = 1)
        {
            var model = new ViewModel
            {
                OrderBy = (PlaceOrderByEnum)orderBy,
                Cities = cities,
                Categories = categories,
                ListType = (ListTypeEnum)listType,
                IsDescending = isDescending,
                Input = input,
                CurrentPage = currentPage
            };
            return View(model);
        }
    }
}
