using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.NewCategory
{
    public class NewCategory : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
