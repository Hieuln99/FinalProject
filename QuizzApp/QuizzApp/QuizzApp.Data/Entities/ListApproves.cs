using System;

namespace QuizzApp.Data.Entities
{
    public class ListApproves
    {
        public Guid Unique { get; set; }
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }
        public Guid? CourseId { get; set; }
        public virtual Course Course { get; set; }
        public DateTime BuyTime { get; set; }
    }
}
