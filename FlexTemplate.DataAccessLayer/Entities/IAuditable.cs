using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Аудируемая сущность
    /// </summary>
    public interface IAuditable : IEntity
    {
        /// <summary>
        /// Время создания
        /// </summary>
        DateTime CreatedOn { get; set; }
        /// <summary>
        /// Время модификации
        /// </summary>
        DateTime ModifiedOn { get; set; }
    }
}
