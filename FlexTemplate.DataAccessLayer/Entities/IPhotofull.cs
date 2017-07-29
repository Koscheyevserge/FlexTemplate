using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность с фотографиями
    /// </summary>
    public interface IPhotofull<TEntity> : IEntity where TEntity : IPhoto
    {
        /// <summary>
        /// BlobKey владельца фотографии
        /// </summary>
        Guid BlobKey { get; set; }
        List<TEntity> Photos { get; set; }
    }
}
