using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Users
    {
        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Userpass { get; set; }
        public bool Isadmin { get; set; }
    }
}
