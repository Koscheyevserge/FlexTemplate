using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedPlaceDao
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public IEnumerable<int> CategoriesIds { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
    }
}
