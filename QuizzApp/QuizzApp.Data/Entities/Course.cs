using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Data.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        [DisplayName("Course Name")]
        [Required(ErrorMessage = "Input course name please!")]
        public string CourseName { get; set; }

        [DisplayName("Create Time")]
        public DateTime CreatedTime { get; set; }
        public IList<TestExam> Tests { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<CategoryCourse> CategoryCourses { get; set; }
    }
}