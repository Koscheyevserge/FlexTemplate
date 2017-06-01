using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FlexTemplate.PresentationLayer.Core
{
    public class FlexViewLocator : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        { }

        public virtual IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new List<string>{ "/WebServices/{0}.cshtml", "/WebServices/{1}/{0}/Index.cshtml"};
        }
    }
}
