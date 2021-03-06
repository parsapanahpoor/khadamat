using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Design_Pattern.GenericRepositories
{
    public interface IGernericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object Id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> where);



        TEntity GetById(object Id);
        IEnumerable<TEntity> GetAll(
                  Expression<Func<TEntity, bool>> filter = null,
                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                  string includeProperties = null
                  );
        TEntity Get(Expression<Func<TEntity, bool>> where);

        IEnumerable<TEntity> GetAllIgnorQueryFilter(
                        Expression<Func<TEntity, bool>> filter = null,
                        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                        string includeProperties = null
                        );

        TEntity GetFirstOrDefault(
               Expression<Func<TEntity, bool>> filter = null,
               string includeProperties = null
               );

    }
}
