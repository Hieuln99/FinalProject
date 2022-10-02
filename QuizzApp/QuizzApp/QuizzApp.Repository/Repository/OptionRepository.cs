using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System.Linq;

namespace QuizzApp.Repository.Repository
{
    public class OptionRepository : BaseRepository<Option>, IOptionRepository
    {
        public OptionRepository(AppDbContext context) : base(context)
        {
        }
        public Option FindByIdString(string key)
        {
            return _context.Options.FirstOrDefault(q => q.Id.ToString() == key);
        }
    }
}
