using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Header
{
    public class Header : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
            /*return View(template,
                new HeaderViewModel
                {
                    Languages = await _context.Languages.ToListAsync(),
                    CurrentLanguage = await _context.Languages
                        .FirstOrDefaultAsync(uc => uc.ShortName == CookieProvider.GetLanguage(HttpContext))
                            ?? _context.Languages.FirstOrDefault(l => l.IsDefault) 
                            ?? new Language(),
                    CurrentUser = await _userManager.GetUserAsync(HttpContext.User)
            });*/
        }
    }
}
