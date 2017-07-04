using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.CityPlaces
{
    public class CityPlaces : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public CityPlaces(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string template)
        {
            var model = await ComponentsServices.GetCityPlacesViewComponentDtoAsync(HttpContext.User, GetType().Name);
            return View(template, model.To<ViewModel>());
        }
    }
}
