using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Answers
{
    public class AnswerModel
    {
        public Guid Id { get; set; }
        [DisplayName("Answer")]
        [Required(ErrorMessage = "Input Answer of the question please!")]
        public string AnswerText { get; set; }
        [DisplayName("User")]
        [Required(ErrorMessage = "Input user please!")]
        public string UserName { get; set; }
        public Guid? QuestionId { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime Summited { get; set; }
        [DisplayName("Question")]
        public virtual Question Question { get; set; }
    }
}
