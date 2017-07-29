using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность с количеством просмотров
    /// </summary>
    public interface IViewable : IEntity
    {
        int ViewsCount { get; set; }
    }
}
