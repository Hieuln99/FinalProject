using QuizzApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Posts
{
    public class PostModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Post title is required!")]
        public string Title { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Input description please!")]
        public string Description { get; set; }

        [DisplayName("Content")]
        [Required(ErrorMessage = "Input content please!")]
        public string Content { get; set; }

        [DisplayName("Post Time")]
        public DateTime PostedOn { get; set; }
        public Guid? UserId { get; set; }

        [DisplayName("User")]
        public virtual User User { get; set; }
        public IList<Comment> Comments { get; set; }
        
    }
}
