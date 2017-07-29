using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class PlaceMenuComponentDao
    {
        public IEnumerable<PlaceMenuComponentMenuDao> Menus { get; set; }
        public bool HasMenus { get; set; }
    }
}
