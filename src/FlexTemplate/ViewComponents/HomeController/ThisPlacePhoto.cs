using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.HomeController
{
    public class ThisPlacePhoto : ViewComponent
    {
        private readonly Context _context;

        public ThisPlacePhoto(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
