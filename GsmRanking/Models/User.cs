using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
        }

        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public byte UserType { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
