using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuizzApp.Data.Entities;

namespace QuizzApp.VModels.Posts
{
    public class UpdatePostVM
    {
        public Post Post { get; set; }
        [ValidateNever]
        public SelectList CategoryList { get; set; }

       
    }
}
