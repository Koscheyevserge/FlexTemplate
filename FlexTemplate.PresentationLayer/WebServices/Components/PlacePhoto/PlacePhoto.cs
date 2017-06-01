using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacePhoto
{
    public class PlacePhoto : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
