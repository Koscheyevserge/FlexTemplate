using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceOverview
{
    public class ViewModel
    {
        public HtmlString Description { get; set; }
        public bool HasSchedule { get; set; }
        public HtmlString Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public ScheduleViewModel Schedule { get; set; }
        public string PlaceCategoriesEnumerated { get; set; }
        public string[] RowsOfFeatures { get; set; }
    }
}
