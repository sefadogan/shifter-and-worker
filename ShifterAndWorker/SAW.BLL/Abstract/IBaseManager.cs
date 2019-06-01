using SAW.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Abstract
{
    public interface IBaseManager<TEntity, TId, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        AppReturn Add(TEntity entity);
        AppReturn Update(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        List<TEntity> ListAll(Expression<Func<TEntity, bool>> filter = null);
    }
}
