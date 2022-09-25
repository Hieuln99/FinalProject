using Microsoft.AspNetCore.Http;
using System;

namespace Models.Account
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Extend { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
