using System;
using System.Collections.Generic;
using System.Text;
using FlexTemplate.DataAccessLayer.Entities;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class GetPlacesListItemPlaceCategoryDao
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public PlaceCategoryAlias CategoryAlias { get; set; }
    }

    public class GetPlacesListItemAddressDao
    {
        public int PlaceId { get; set; }
        public string Address { get; set; }
        public string StreetName { get; set; }
        public StreetAlias StreetAlias { get; set; }
        public string CityName { get; set; }
        public CityAlias CityAlias { get; set; }
    }

    public class GetPlacesListItemPlaceDao
    {
        public int PlaceId { get; set; }
        public IEnumerable<GetPlacesListItemPlaceCategoryDao> Categories { get; set; }
    }
}
