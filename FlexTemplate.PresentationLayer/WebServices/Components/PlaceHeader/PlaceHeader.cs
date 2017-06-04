using System;
using System.Threading.Tasks;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceHeader
{
    public class PlaceHeader : FlexViewComponent
    {
        public IViewComponentResult Invoke(int placeId)
        {
            //var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            //return View(new ThisPlaceHeaderViewModel { CanEdit = currentUser == item.User, Place = item , Stars = item.Reviews.Any(p => p.Star > 0) ? Math.Ceiling(item.Reviews.Where(p => p.Star > 0).Average(p => p.Star)) : 0 });
            return View();
        }
    }
}