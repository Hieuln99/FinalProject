using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;

namespace QuizzApp.Repository.IRepository
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        public Question FindByIdString(string key);
    }
}
