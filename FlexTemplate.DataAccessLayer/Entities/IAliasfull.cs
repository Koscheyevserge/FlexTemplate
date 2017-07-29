using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public interface IAliasfull<TEntity> : IEntity where TEntity : IAlias
    {
        string Name { get; set; }
        List<TEntity> Aliases { get; set; }
    }
}
