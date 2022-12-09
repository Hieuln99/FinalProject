using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Options
{
    public class CreateOptionVM
    {
        [Required(ErrorMessage = "Input option please!")]
        public List<string> Options { get; set; }
        [Required(ErrorMessage = "Input question please!")]
        public string QuestionName { get; set; }
        [Required(ErrorMessage = "Please choose right answer!")]
        public bool Status1 { get; set; }
        public bool Status2 { get; set; }
        public bool Status3 { get; set; }
        public bool Status4 { get; set; }
        public List<bool> Status { get;  set; }

        [Required(ErrorMessage = "Please choose a course!")]
        public Guid CourseId { get; set; }

    }
}
