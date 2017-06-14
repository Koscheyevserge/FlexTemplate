using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CityChecklistItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public IEnumerable<int> CitiesWithoutThisIds { get; set; }
    }
}
