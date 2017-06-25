using System.Linq;
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

        public IViewComponentResult Invoke(int placesCount)
        {
            int.TryParse(Request.Query["currentPage"], out int currentPage);
            var input = Request.Query["input"].ToString();
            int.TryParse(Request.Query["orderBy"], out int orderBy);
            var cities = Request.Query["cities"].ToArray().Select(int.Parse);
            var categories = Request.Query["categories"].ToArray().Select(int.Parse);
            bool.TryParse(Request.Query["isDescending"], out bool isDescending);
            int.TryParse(Request.Query["listType"], out int listType);
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
