using QuizzApp.Data.Entities;
using System;

namespace QuizzApp.VModels.Comments
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string CommentHeader { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public Guid? PostId { get; set; }
        public Post Post { get; set; }
    }
}
