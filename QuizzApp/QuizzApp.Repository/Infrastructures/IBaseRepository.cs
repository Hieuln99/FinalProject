using System.Collections.Generic;
using System.Linq;

namespace QuizzApp.Repository.Infrastructures
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        void AddRange(List<TEntity> entities);

        TEntity GetById(params object[] primarykey);

        IQueryable<TEntity> GetAll();

        void Remove(TEntity entity);

        void RemoveRange(List<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(List<TEntity> entities);
    }
}
