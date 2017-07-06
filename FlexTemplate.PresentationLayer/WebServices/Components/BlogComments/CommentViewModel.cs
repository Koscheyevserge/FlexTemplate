using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogComments
{
    public class CommentViewModel
    {
        public DateTime CreatedOn { get; set; }
        public string AuthorPhotoPath { get; set; }
        public string Text { get; set; }
        public string AuthorUsername { get; set; }
    }
}
