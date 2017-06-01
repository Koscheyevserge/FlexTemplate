using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceLocation
{
    public class PlaceLocation : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
