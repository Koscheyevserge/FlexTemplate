using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class NewPlacePageDao
    {
        public string BannerPhotoPath { get; set; }
        public Guid NewPlaceGuid { get; set; }
        public IEnumerable<NewPlacePageCategoryDao> Categories { get; set; }
    }
}
