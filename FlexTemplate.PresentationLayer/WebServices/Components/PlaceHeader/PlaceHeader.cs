using System;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceHeader
{
    public class PlaceHeader : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlaceHeader(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int placeId);
            var model = await ComponentsServices.GetPlaceHeaderAsync(HttpContext.User, placeId);
            return View(model.To<ViewModel>());
        }
    }
}