using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.MorePlaces
{
    public class MorePlaces : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public MorePlaces(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<int> loadedPlacesIds)
        {
            var model = await ComponentsServices.GetMorePlacesAsync(loadedPlacesIds);
            return View(model.To<ViewModel>());
        }
    }
}
