using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.IRepository
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        public int CountPostsForCategory(string category);
        IList<Post> GetLatestPost(int size);
        IList<Post> GetPostsByMonth(DateTime monthYear);
        IList<Post> GetPostsByCategory(string category);
        IList<Post> GetMostViewedPost(int size);
        IList<Post> GetHighestPosts(int size);

        IEnumerable<Post> GetAll2(Expression<Func<Post, bool>>? filter = null, string? includeProperties = null);
    }
}
