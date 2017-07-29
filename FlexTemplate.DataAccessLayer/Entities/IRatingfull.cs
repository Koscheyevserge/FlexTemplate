using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность с оценкой
    /// </summary>
    public interface IRatingfull : IEntity
    {
        /// <summary>
        /// Оценка
        /// </summary>
        int? Rating { get; set; }
    }
}
