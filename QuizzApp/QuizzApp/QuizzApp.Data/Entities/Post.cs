using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.Data.Entities
{
    public class Post
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
        public bool? Published { get; set; }

        [DisplayName("Post Time")]
        public DateTime? PostedOn { get; set; }
        public DateTime? Modified { get; set; }
        public int? ViewCount { get; set; } = 0;
        public int? RateCount { get; set; } = 0;
        public int? TotalRate { get; set; } = 0;
        public decimal Rate
        {
            get
            {
                if (TotalRate == null || RateCount == null)
                    return 0;
                if (TotalRate.Value == 0 || RateCount.Value == 0)
                    return 0;
                return Convert.ToDecimal(1.0 * TotalRate.Value / RateCount.Value);
            }
        }
        public Guid? CategoryId { get; set; }
        public Guid? UserId { get; set; }

        [DisplayName("User")]
        public virtual User User { get; set; }
        public IList<Comment> Comments { get; set; }
        public Category Category { get; set; }
    }
}
