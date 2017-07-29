using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogComments
{
    public class ViewModel
    {
        public int CommentsCount { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}
