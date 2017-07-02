using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceMenu
{
    public class PlaceMenu : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlaceMenu(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int placeId);
            var model = await ComponentsServices.GetPlaceMenusAsync(placeId);
            return View(model.To<ViewModel>());
        }
    }
}