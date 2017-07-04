namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас улицы
    /// </summary>
    public class StreetAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор улицы
        /// </summary>
        public int StreetId { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public Street Street { get; set; }
    }
}
