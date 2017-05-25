using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Identity;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlaceHeader : ViewComponent
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;

        public ThisPlaceHeader(Context context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Place item)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            return View(new ThisPlaceHeaderViewModel { CanEdit = currentUser == item.User, Place = item , Stars = item.Reviews.Any(p => p.Star > 0) ? Math.Ceiling(item.Reviews.Where(p => p.Star > 0).Average(p => p.Star)) : 0 });
        }
    }
}