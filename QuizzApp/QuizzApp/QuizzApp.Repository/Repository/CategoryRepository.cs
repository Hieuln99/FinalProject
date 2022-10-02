using Microsoft.EntityFrameworkCore;
using QuizzApp.Data.DbContext;
using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using QuizzApp.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;


namespace QuizzApp.Repository.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }


        public IEnumerable<Course> GetCourseOfCategory(Guid id)
        {
            IList<Course> courses = new List<Course>();
            var listIdCourse = _context.CategoryCourses.Where(cc => cc.CategoryId == id).ToList();
            foreach (var item in listIdCourse)
            {
                courses.Add(_context.Courses.FirstOrDefault(c => c.Id == item.CourseId));
            }
            return courses;
        }
        public IEnumerable<Category> GetAll2(Expression<Func<Category, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Category> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();

        }
    }
}