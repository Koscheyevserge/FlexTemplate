using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedPlaceDao
    {
        public int Id { get; set; }
        public IEnumerable<CachedCategoryDao> Categories { get; set; }
        public IEnumerable<CachedPlaceNameDao> Names { get; set; }
        public CachedCityDao City { get; set; }
        public int ViewsCount { get; set; }
        public double Rating { get; set; }
        public string Name { get; set; }
    }
}
