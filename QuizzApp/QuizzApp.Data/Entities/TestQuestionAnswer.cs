using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Data.Entities
{
    //testOptions
    public class TestQuestionAnswer
    {
        public Guid Id { get; set; }
        [DisplayName("Answer")]
        [Required(ErrorMessage = "Input Answer of the question please!")]
        //public Guid TestId { get; set; }
        //public virtual TestExam Test { get; set; }

        public DateTime Summited { get; set; }
        public Guid? AnswerId { get; set; }
        public Guid? TestId { get; set; }
        public Guid? QuestionId { get; set; }
        public virtual TestQuestion TestQuestion { get; set; }     
        public virtual Question Question { get; set; }
        public virtual Option Answer { get; set; }
        //public virtual User User { get; set; }
    }
}
