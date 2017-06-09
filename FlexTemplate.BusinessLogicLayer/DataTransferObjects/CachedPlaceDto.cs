using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedPlaceDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public IEnumerable<int> CategoriesIds { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
    }
}
