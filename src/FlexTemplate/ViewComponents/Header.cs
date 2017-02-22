using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents
{
    public class Header : ViewComponent
    {
        public IViewComponentResult Invoke(string template)
        {
            return View(string.IsNullOrEmpty(template) ? "Default" : template);
        }
    }
}
