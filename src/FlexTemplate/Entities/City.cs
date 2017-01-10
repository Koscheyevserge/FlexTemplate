namespace FlexTemplate.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Город
    /// </summary>
    public class City : BaseEntity
    {
        /// <summary>
        /// Название города
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор страны, в которой находится город
        /// </summary>
        public int CountryId { get; set; }
        /// <summary>
        /// Страна, в которой находится город
        /// </summary>
        public virtual Country Country { get; set; }
    }
}
