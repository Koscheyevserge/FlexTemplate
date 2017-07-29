using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность фотографии
    /// </summary>
    public interface IPhoto : IEntity
    {
        /// <summary>
        /// BlobKey сущности-владельца фотографии
        /// </summary>
        Guid BlobKey { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        string Uri { get; set; }
        /// <summary>
        /// Активен
        /// </summary>
        bool IsActive { get; set; }
    }
}
