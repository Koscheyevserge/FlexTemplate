using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class Capabilities : ViewComponent
    {
        public IViewComponentResult Invoke(string template)
        {
            return View(template);
        }
    }
}
