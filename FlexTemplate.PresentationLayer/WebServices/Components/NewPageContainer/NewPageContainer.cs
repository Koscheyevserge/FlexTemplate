using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.NewPageContainer
{
    public class NewPageContainer : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}