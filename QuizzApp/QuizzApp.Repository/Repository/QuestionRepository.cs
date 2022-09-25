using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System.Linq;

namespace QuizzApp.Repository.Repository
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }
        public Question FindByIdString(string key)
        {
            return _context.Questions.FirstOrDefault(q => q.Id.ToString() == key);
        }
    }
}
