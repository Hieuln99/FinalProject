using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;

namespace QuizzApp.VModels.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        //public IList<CategoryCourse> CategoryCourses { get; set; }
    }
}