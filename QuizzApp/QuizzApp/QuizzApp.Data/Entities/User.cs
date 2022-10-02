using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace QuizzApp.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public IList<Post> Posts { get; set; }
        public IList<Category> Categories { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }
        public string Extend { get; set; }
        public byte[] Image { get; set; }
    }
}