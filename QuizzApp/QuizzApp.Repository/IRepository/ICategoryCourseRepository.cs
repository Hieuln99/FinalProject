using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizzApp.Repository.IRepository
{
    public interface ICategoryCourseRepository : IBaseRepository<CategoryCourse>
    {
    }
}
