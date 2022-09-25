using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Input category name please!")]
        [DisplayName("Name of Category")]
        public string CategoryName { get; set; }
        public Guid? UserId { get; set; }
        [Display(Name ="User")]
        public virtual User User { get; set; }
        public IList<CategoryCourse> CategoryCourses { get; set; }
    }
}
