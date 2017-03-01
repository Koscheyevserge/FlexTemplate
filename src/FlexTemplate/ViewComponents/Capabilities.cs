using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents
{
    public class Capabilities : ViewComponent
    {
        public IViewComponentResult Invoke(string template)
        {
            return View(template);
        }
    }
}
