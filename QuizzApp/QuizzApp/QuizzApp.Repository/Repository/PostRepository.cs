using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context)
        {
        }

       
        public int CountPostsForCategory(string category)
        {
            return base._dbSet.Where(p => p.Category.CategoryName == category).Count(); 
        }

        public IEnumerable<Post> GetAll2(Expression<Func<Post, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Post> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public IList<Post> GetHighestPosts(int size)
        {
            return base._dbSet.Include(x => x.Category)
                              .OrderByDescending(p => (p.ViewCount.Value == 0 || p.RateCount.Value == 0 || p.ViewCount == 0 || p.RateCount == 0) ? 0 : Convert.ToDecimal(1.0 * p.TotalRate.Value / p.RateCount.Value))
                              .Take(size).ToList();
        }                      

        public IList<Post> GetLatestPost(int size)
        {
            return base._dbSet.Include(x => x.Category).OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }

        public IList<Post> GetMostViewedPost(int size)
        {
            return base._dbSet.Include(x => x.Category).OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }

        public IList<Post> GetPostsByCategory(string category)
        {
            return base._dbSet.Include(x => x.Category).Where(p => p.Category.CategoryName == category).ToList();
        }

        public IList<Post> GetPostsByMonth(DateTime monthYear)
        {
            return base._dbSet.Where(p => p.PostedOn.Value.Year == monthYear.Year && p.PostedOn.Value.Month == monthYear.Month)
                             .ToList();
        }
    }
}
