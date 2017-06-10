using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedPlaceDto
    {
        public int Id { get; set; }
        public IEnumerable<CachedCategoryDto> Categories { get; set; }
        public IEnumerable<CachedPlaceNameDto> Names { get; set; }
        public CachedCityDto City { get; set; }
        public int ViewsCount { get; set; }
        public double Rating { get; set; }
    }
}
