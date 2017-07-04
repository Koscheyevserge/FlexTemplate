using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCityPlaces
{
    public class OtherCityPlaces : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public OtherCityPlaces(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int cityId)
        {
            var model = await ComponentsServices.GetOtherCityPlacesViewComponentDtoAsync(HttpContext.User, GetType().Name, cityId);
            return View(model.To<ViewModel>());
        }
    }
}
