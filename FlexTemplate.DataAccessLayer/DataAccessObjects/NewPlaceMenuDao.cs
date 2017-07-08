using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class NewPlaceMenuDao
    {
        public string Name { get; set; }
        public IEnumerable<NewPlaceProductDao> Products { get; set; }
    }
}
