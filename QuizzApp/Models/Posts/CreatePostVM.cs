using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Posts
{
    public class CreatePostVM
    {
        [Required(ErrorMessage = "Title không được để trống")]
        public string Title { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Content")]
        [Required(ErrorMessage = "Input content please!")]
        public string Content { get; set; }
        [DisplayName("Publish On")]
        public bool Published { get; set; }
        [DisplayName("Tags")]
        public string Tags { get; set; }
        public Guid CategoryId { get; set; }
    }
}
