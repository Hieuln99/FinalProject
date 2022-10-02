using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;

namespace QuizzApp.VModels.Posts
{
    public class PostViewModel
    {

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
