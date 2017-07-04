using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.NewPlaceProduct
{
    public class NewPlaceProduct : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
