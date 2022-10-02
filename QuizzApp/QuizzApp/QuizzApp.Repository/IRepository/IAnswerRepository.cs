using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;

namespace QuizzApp.Repository.IRepository
{
    public interface IAnswerRepository : IBaseRepository<TestQuestionAnswer>
    {
    }
}
