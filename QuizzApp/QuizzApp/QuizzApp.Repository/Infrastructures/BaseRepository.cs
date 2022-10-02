using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using System.Collections.Generic;
using System.Linq;

namespace QuizzApp.Repository.Infrastructures
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<TEntity> _dbSet;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public TEntity GetById(params object[] primarykey)
        {
            return _dbSet.Find(primarykey);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            _dbSet.Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}