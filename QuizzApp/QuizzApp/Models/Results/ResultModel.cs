using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Results
{
    public class ResultModel
    {
        public Guid Id { get; set; }

        [DisplayName("Answer")]
        [Required(ErrorMessage = "Input answer please!")]
        public string AnswerText { get; set; }
        public Guid? QuestionId { get; set; }

        [DisplayName("Question")]
        public virtual Question Question { get; set; }
    }
}
