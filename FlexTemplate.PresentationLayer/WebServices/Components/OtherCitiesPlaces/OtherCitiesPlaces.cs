using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.OtherCitiesPlaces
{
    public class OtherCitiesPlaces : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public OtherCitiesPlaces(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string template)
        {
            var model = await ComponentsServices.GetOtherCitiesPlacesViewComponentDtoAsync(HttpContext.User, GetType().Name);
            return View(template, model.To<ViewModel>());
        }
    }
}
