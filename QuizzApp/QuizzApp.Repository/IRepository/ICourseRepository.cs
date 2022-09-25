using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;

namespace QuizzApp.Repository.IRepository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        public Course FindByIdString(string key);
    }
}
