using QuizzApp.Data.Entities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizzApp.VModels.Options
{
    public class OptionVModel
    {
        public Guid Id { get; set; }
        [DisplayName("Option Name")]
        [Required(ErrorMessage = "Input Option name please!")]
        public string OptionName { get; set; }
        
        public virtual Question Question { get; set; }
    }
}
