using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReview
{
    public class ViewModel
    {
        public string UserPhotoPath { get; set; }
        public string UserName { get; set; }
        public int Stars { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
