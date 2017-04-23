using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
            Comments = new HashSet<Comments>();
        }

        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public byte UserType { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
