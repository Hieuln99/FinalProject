using System;
using System.Collections.Generic;

namespace QuizzApp.Data.Entities
{
    public class TestExam
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public Guid? CourseId { get; set; }
        public Guid UserId { get; set; }
        public virtual IList<TestQuestion> TestQuestions { get; set; }
        public DateTime TakeOn { get; set; }
    }
}
