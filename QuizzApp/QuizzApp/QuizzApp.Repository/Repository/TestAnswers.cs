using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;

namespace QuizzApp.Repository.Repository
{
    public class TestAnswers : BaseRepository<TestQuestionAnswer>, ITestAnswers
    {
        public TestAnswers(AppDbContext context) : base(context)
        {
        }
    }
}
