using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Модерируемая сущность
    /// </summary>
    public interface IModerateable : IEntity
    {
        /// <summary>
        /// Модерирован
        /// </summary>
        bool IsModerated { get; set; }
    }
}
