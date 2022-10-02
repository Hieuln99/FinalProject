using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.Repository
{
    public class CategoryCourseRepository : BaseRepository<CategoryCourse>, ICategoryCourseRepository
    {
        public CategoryCourseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
