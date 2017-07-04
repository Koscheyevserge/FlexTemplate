using System.Collections.Generic;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogComments
{
    public class BlogComments : FlexViewComponent
    {
        public IViewComponentResult Invoke(int blogId)
        {
            return View();
        }
    }
}
