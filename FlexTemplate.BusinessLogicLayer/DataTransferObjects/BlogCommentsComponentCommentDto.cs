using System;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogCommentsComponentCommentDto
    {
        public DateTime CreatedOn { get; set; }
        public string AuthorPhotoPath { get; set; }
        public string Text { get; set; }
        public string AuthorUsername { get; set; }
    }
}
