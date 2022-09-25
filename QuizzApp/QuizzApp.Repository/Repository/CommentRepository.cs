using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public void AddComment(Guid postId, string commentEmail, string commentTitle, string commentBody)
        {
            base._dbSet.Add(new Comment
            {
                PostId = postId,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody
            });
        }

        public IList<Comment> GetCommentsForPost(Guid postId)
        {
            return base._dbSet.Where(x => x.PostId == postId).ToList();
        }

        public IList<Comment> GetCommentsForPost(Post post)
        {
            return this._dbSet.Where(x => x.PostId == post.Id).ToList();
        }
    }
}
