using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexTemplate.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Context context { get; set; }

        protected BaseController(Context Context)
        {
            context = Context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["HeaderTemplate"] = "Solid";
            base.OnActionExecuting(context);
        }
    }
}
