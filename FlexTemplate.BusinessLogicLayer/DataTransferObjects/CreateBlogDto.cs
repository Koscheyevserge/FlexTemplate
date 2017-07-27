using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CreateBlogDto
    {
        public string Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BannersKey { get; set; }
        public IEnumerable<int> Categories { get; set; }
    }
}
