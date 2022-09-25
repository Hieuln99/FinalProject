using QuizzApp.Data.Entities;
using QuizzApp.Repository.Infrastructures;
using System;
using System.Collections.Generic;


using System.Linq.Expressions;


namespace QuizzApp.Repository.IRepository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {

        public IEnumerable<Course> GetCourseOfCategory(Guid id);

        IEnumerable<Category> GetAll2(Expression<Func<Category, bool>>? filter = null, string? includeProperties = null);

    }
}