using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class Suggestions : ViewComponent
    {
        public IViewComponentResult Invoke(string template)
        {
            return View(template);
        }
    }
}
