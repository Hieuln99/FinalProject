using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;

namespace QuizzApp.VModels.Results
{
    public class ResultVModel
    {
        public Guid Id { get; set; }

        [DisplayName("Answer")]
        public string AnswerText { get; set; }

        [DisplayName("Question")]
        public virtual Question Question { get; set; }
    }
}
