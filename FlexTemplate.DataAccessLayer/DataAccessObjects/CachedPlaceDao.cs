using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CachedPlaceDao
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
        public string CityName { get; set; }
        public int ViewsCount { get; set; }
        public double Rating { get; set; }
    }
}
