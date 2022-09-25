using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System.Linq;

namespace QuizzApp.Repository.Repository
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }
        public Course FindByIdString(string key)
        {
            return _context.Courses.FirstOrDefault(q => q.Id.ToString() == key);
        }
    }
}
