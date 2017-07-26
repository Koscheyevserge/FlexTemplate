using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CreateBlogDao
    {
        public List<string> Tags { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid BannersKey { get; set; }
    }
}
