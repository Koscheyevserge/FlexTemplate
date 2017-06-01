using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlaceFeatureColumn : BaseEntity
    {
        /// <summary>
        /// Заведение
        /// </summary>
        public Place Place { get; set; }

        public int PlaceId { get; set; }

        public virtual List<PlaceFeature> Features { get; set; }
    }
}
