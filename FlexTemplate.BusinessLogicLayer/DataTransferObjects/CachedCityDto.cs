using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CachedCityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CachedCityNameDto> Names { get; set; }
    }
}
