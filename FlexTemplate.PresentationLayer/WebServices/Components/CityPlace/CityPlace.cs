using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlace
{
    public class CityPlace : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public CityPlace(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int placeId)
        {
            var model = await ComponentsServices.GetCityPlaceViewComponentDtoAsync(HttpContext.User, GetType().Name, placeId);
            return View(model.To<ViewModel>());
        }
    }
}
