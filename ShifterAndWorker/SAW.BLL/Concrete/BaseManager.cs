using SAW.BLL.Abstract;
using SAW.BLL.Utilities.Log4net;
using SAW.Core;
using SAW.DAL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SAW.BLL.Concrete
{
    public class BaseManager<TEntity, TId, TContext> : IBaseManager<TEntity, TId, TContext>
        where TEntity : class, new()
        where TContext : DbContext
    {
        private readonly DbContext _context;

        public BaseManager(DbContext context)
        {
            _context = context;
        }

        private IDbSet<TEntity> DbSet
        {
            get { return _context.Set<TEntity>(); }
        }
        public AppReturn Add(TEntity entity)
        {
            try
            {
                DbSet.Add(entity);

                Log.Info("Successfully added model.");
                return AppReturn.Successful();
            }
            catch (Exception e)
            {
                Log.Error("An unexpected error has occured while adding model.", e);
                return AppReturn.InvalidOperation(e.Message);
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }
        public List<TEntity> ListAll(Expression<Func<TEntity, bool>> filter = null)
        {
            List<TEntity> list;
            if (filter == null)
            {
                list = DbSet.ToList();
            }
            else
            {
                list = DbSet.Where(filter).ToList();
            }
            return list;
        }
        public AppReturn Update(TEntity entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                Log.Info($"{ entity.GetType() } updated successfuly.");
                return AppReturn.Successful();
            }
            catch (Exception e)
            {
                Log.Error($"An unexpected error has occured while updating { entity.GetType() }.", e);
                return AppReturn.InvalidOperation(e.Message);
            }
        }
    }
}
