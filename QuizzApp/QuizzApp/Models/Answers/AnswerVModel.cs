using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;

namespace QuizzApp.VModels.Answers
{
    public class AnswerVModel
    {
        public Guid Id { get; set; }
        [DisplayName("Answer")]
        public string AnswerText { get; set; }
        [DisplayName("User")]
        public string UserName { get; set; }
        public Guid? CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime Summited { get; set; }
        [DisplayName("Question")]
        public virtual Question Question { get; set; }
    }
}
