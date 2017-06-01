namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас города
    /// </summary>
    public class CityAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор города
        /// </summary>
        public int CityId { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public City City { get; set; }
    }
}
