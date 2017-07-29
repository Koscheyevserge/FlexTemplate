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
        
        [Route("api/loadmoreplaces")]
        [HttpPost]
        public IActionResult LoadMorePlaces([FromBody]LoadMorePlaces.PostModel data)
        {
            return ViewComponent(typeof(Components.MorePlaces.MorePlaces), new { loadedPlacesIds = data.LoadedPlacesIds });
        }

        [Route("api/loadmenu")]
        [HttpPost]
        public IActionResult LoadMenu([FromBody]NewPlaceNewMenu.PostModel model)
        {
            return ViewComponent(typeof(Components.NewPlaceMenu.NewPlaceMenu), new { position = model.Position });
        }

        [Route("api/loadproduct")]
        [HttpPost]
        public IActionResult LoadProduct([FromBody]NewPlaceNewProduct.PostModel model)
        {
            return ViewComponent(typeof(Components.NewPlaceProduct.NewPlaceProduct), new { position = model.Position, menu = model.Menu });
        }
    }
}
