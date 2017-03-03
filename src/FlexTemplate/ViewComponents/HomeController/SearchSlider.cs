using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class SearchSlider : ViewComponent
    {
        private readonly Context _context;

        public SearchSlider(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string template)
        {
            return View(template);
        }
    }
}
