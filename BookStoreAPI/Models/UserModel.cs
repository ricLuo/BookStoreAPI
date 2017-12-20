using System;

namespace BookStoreAPI.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}