using FlexTemplate.Database;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Controllers
{
    public class ComponentsController : BaseController
    {
        public ComponentsController(Context Context) : base(Context)
        {
            context = Context;
        }

        [Route("api/loadmoreplaces")]
        [HttpPost]
        public IActionResult LoadMorePlaces([FromBody]LoadMorePlacesViewModel data)
        {
            return ViewComponent("MorePlaces", new { loadedPlacesIds = data.LoadedPlacesIds });
        }
    }
}
