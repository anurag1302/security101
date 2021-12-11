using System;

namespace web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Role { get; set; }
        public string FavColor { get; set; }
    }
}
