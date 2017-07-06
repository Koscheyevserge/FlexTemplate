using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Home.NewPlace
{
    public class ViewModel
    {
        public string BannerPhotoPath { get; set; }
        public Guid NewPlaceGuid { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
