using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuizzApp.VModels.Questions
{
    public class QuestionEditVM
    {
        public Guid Id { get; set; }

        [DisplayName("Question")]
        public string QuestionName { get; set; }

        [DisplayName("Course")]
        public virtual Course Course { get; set; }
        public virtual IList<Option> Options { get; set; }
    }
}
