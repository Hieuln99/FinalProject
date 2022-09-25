using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Courses
{
    public class CourseModel
    {
        public Guid Id { get; set; }

        [DisplayName("Course Name")]
        [Required(ErrorMessage = "Input course name please!")]
        public string CourseName { get; set; }

        [DisplayName("Create Time")]
        public DateTime CreatedTime { get; set; }
        public virtual IList<Question> Questions { get; set; }
        //public virtual IList<CategoryCourse> CategoryCourses { get; set; }
    }
}
