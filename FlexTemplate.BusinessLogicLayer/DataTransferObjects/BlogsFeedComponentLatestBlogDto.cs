using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogsFeedComponentLatestBlogDto
    {
        public string HeadPhotoPath { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Id { get; set; }
    }
}
