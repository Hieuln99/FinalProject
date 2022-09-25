using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Questions
{
    public class QuestionModel
    {
        public Guid Id { get; set; }

        [DisplayName("Question")]
        [Required(ErrorMessage = "Input question please!")]
        public string QuestionName { get; set; }

        [DisplayName("Allow multiple choice")]
        public bool IsMultiple { get; set; }
        public Guid? CourseId { get; set; }

        [DisplayName("Course")]
        public virtual Course Course { get; set; }
    }
}
