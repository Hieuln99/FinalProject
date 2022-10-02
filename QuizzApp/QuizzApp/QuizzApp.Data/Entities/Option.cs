using System;

namespace QuizzApp.Data.Entities
{
    public class Option
    {
        public Guid Id { get; set; }
        public string OptionName { get; set; }
        public Guid? QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public bool Status { get; set; }
    }
}
