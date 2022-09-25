using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuizzApp.VModels.Courses
{
    public class CourseVModel
    {
        public Guid Id { get; set; }

        [DisplayName("Course Name")]
        public string CourseName { get; set; }

        [DisplayName("Create Time")]
        public DateTime CreatedTime { get; set; }
        public virtual IList<Question> Questions { get; set; }
        //public virtual IList<CategoryCourse> CategoryCourses { get; set; }
    }
}
