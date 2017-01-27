using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
