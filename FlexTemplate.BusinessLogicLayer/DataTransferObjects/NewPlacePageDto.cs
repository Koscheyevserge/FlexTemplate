using System;
using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class NewPlacePageDto
    {
        public string BannerPhotoPath { get; set; }
        public Guid NewPlaceGuid { get; set; }
        public IEnumerable<NewPlacePageCategoryDto> Categories { get; set; }
    }
}
