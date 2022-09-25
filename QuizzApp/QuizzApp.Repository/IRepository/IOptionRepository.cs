using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;

namespace QuizzApp.Repository.IRepository
{
    public interface IOptionRepository : IBaseRepository<Option>
    {
        public Option FindByIdString(string key);
    }
}
