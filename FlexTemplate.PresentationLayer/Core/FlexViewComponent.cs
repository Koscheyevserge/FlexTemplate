﻿using System.Collections.Generic;
using System.Linq;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace FlexTemplate.PresentationLayer.Core
{
    public abstract class FlexViewComponent : ViewComponent
    {
        protected Dictionary<string, object> RouteValues { get; set; }

        protected FlexViewComponent()
        {
            RouteValues = ViewContext?.RouteData?.Values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
