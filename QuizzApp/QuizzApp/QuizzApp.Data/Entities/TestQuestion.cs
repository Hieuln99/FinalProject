using System;
using System.Collections.Generic;

namespace QuizzApp.Data.Entities
{
    public class TestQuestion
    {
        public Guid TestId { get; set; }
        public Guid TestExamId { get; set; }
        public Guid QuestionId { get; set; }
        public virtual TestExam Test { get; set; }
        public virtual Question Question { get; set; }
        public virtual IList<TestQuestionAnswer> Answers { get; set; }
    }
}
