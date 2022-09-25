using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;

namespace QuizzApp.Repository.Repository
{
    public class AnswerRepository : BaseRepository<TestQuestionAnswer>, IAnswerRepository
    {
        public AnswerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
