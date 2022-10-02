using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Comments
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Input user email please!")]
        public string Email { get; set; }

        [DisplayName("Header")]
        [Required(ErrorMessage = "Input header please!")]
        public string CommentHeader { get; set; }

        [DisplayName("Text")]
        [Required(ErrorMessage = "Input comment content please!")]
        public string CommentText { get; set; }

        [DisplayName("Time")]
        public DateTime CommentTime { get; set; }
        public Guid? PostId { get; set; }

        [DisplayName("Post")]
        public Post Post { get; set; }
    }
}
