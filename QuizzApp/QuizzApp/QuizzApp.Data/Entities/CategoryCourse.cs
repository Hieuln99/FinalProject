using System;

namespace QuizzApp.Data.Entities
{
    public class CategoryCourse
    {
        public Guid CourseId { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Course Course { get; set; }
        public virtual Category Category { get; set; }
    }
}