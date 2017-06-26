using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceOverview
{
    public class PlaceOverview : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlaceOverview(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int placeId);
            var model = await ComponentsServices.GetPlaceOverviewAsync(HttpContext.User, placeId);
            return View(model.To<ViewModel>());
        }
    }
}