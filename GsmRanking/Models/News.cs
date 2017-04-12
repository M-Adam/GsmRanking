using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class News
    {
        public int IdNews { get; set; }
        public int IdAutor { get; set; }
        public string Shorttext { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime Createdate { get; set; }
        public DateTime? Publishdate { get; set; }
        public bool Ispublished { get; set; }
        public int Viewscount { get; set; }
        public string Image { get; set; }
    }
}
