using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexTemplate.PresentationLayer.Core
{
    public abstract class FlexController : Controller
    {
        protected BusinessLogicLayer.Services.ControllerServices BllServices { get; }

        protected FlexController(BusinessLogicLayer.Services.ControllerServices services)
        {
            BllServices = services;
        }
    }
}
