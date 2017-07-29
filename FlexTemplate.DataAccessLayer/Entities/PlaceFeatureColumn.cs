using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceFeatureColumn : IEntity
    {
        /// <summary>
        /// Заведение
        /// </summary>
        public Place Place { get; set; }

        public int PlaceId { get; set; }

        public int Position { get; set; }

        public virtual List<PlaceFeature> Features { get; set; }
        public int Id { get; set; }
    }
}
