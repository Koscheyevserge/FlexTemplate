using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Search
{
    public class ViewModel
    {
        public string BackgroundImagePath { get; set; }
        public HtmlString TitleFirstLabelCaption { get; set; }
        public HtmlString SubtitleLabelCaption { get; set; }
        public HtmlString FindButtonCaption { get; set; }
        public HtmlString EndLabelCaption { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Cities { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }
}
