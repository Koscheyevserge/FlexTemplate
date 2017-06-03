﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Search
{
    public class ViewModel
    {
        public string BackgroundImagePath { get; set; }
        public string TitleFirstLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
        public string FindButtonCaption { get; set; }
        public string EndLabelCaption { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Cities { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }
}
