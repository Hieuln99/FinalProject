using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Data.Entities
{
    public class Question
    {
        public Guid Id { get; set; }

        [DisplayName("Question")]
        [Required(ErrorMessage = "Input question please!")]
        public string QuestionName { get; set; }

        [DisplayName("Allow multiple choice")]
        public bool IsMultiple { get; set; }
        public int Number { get; set; }
        public Guid? CourseId { get; set; }

        [DisplayName("Course")]
        public virtual Course Course { get; set; }
        public virtual IList<TestQuestion> TestQuestions { get; set; }
        public virtual IList<Option> Options { get; set; }
        //public virtual IList<Result> Results { get; set; }
    }
}
