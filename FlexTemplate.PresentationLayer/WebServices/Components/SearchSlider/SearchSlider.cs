using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.SearchSlider
{
    public class SearchSlider : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
