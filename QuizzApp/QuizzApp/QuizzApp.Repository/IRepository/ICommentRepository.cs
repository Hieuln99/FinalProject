using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.IRepository
{
    public interface ICommentRepository :  IBaseRepository<Comment>
    {
        void AddComment(Guid postId, string commentEmail, string commentTitle, string commentBody);

        IList<Comment> GetCommentsForPost(Guid postId);

        IList<Comment> GetCommentsForPost(Post post);
    }
}
