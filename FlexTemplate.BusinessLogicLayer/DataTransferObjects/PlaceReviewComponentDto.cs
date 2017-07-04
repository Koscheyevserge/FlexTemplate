using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceReviewComponentDto
    {
        public string UserPhotoPath { get; set; }
        public string UserName { get; set; }
        public int Stars { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
