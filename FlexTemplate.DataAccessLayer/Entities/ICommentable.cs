using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public interface ICommentable<TEntity> : IEntity where TEntity : IComment
    {
        List<TEntity> Comments { get; set; }
    }
}
