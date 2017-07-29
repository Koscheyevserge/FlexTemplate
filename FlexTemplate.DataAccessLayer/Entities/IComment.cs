using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность комментария
    /// </summary>
    public interface IComment : IAuthorfull, IAuditable, IModerateable
    {
        /// <summary>
        /// Текст комментария
        /// </summary>
        string Text { get; set; }
    }
}
