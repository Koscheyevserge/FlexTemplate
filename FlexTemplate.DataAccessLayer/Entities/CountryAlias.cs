namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Алиас страны
    /// </summary>
    public class CountryAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор страны
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public Country Country { get; set; }
    }
}
