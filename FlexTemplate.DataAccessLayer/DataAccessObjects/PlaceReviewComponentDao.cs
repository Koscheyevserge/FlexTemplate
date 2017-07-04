using System;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PlaceReviewComponentDao
    {
        public string UserPhotoPath { get; set; }
        public string UserName { get; set; }
        public int Stars { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
