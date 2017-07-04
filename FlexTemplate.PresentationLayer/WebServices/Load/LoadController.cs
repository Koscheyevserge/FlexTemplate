using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Load
{
    public class LoadController : FlexController
    {
        public LoadController(ControllerServices services) : base(services)
        {
        }
        /*
        [Route("api/loadmoreplaces")]
        [HttpPost]
        public IActionResult LoadMorePlaces([FromBody]LoadMorePlacesViewModel data)
        {
            return ViewComponent(typeof(Components.MorePlaces.MorePlaces), new { loadedPlacesIds = data.LoadedPlacesIds });
        }

        [Route("api/loadmenu")]
        [HttpPost]
        public IActionResult LoadMenu([FromBody]NewPlaceNewMenuViewModel model)
        {
            return ViewComponent("NewPlaceMenu", new { model = model });
        }

        [Route("api/loadproduct")]
        [HttpPost]
        public IActionResult LoadProduct([FromBody]NewPlaceNewProductViewModel model)
        {
            return ViewComponent("NewPlaceProduct", new { model = model });
        }*/
    }
}
