using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public int IdAutor { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public int? IdNews { get; set; }
        public int? IdArticle { get; set; }
        public int? IdPhone { get; set; }

        public virtual Article IdArticleNavigation { get; set; }
        public virtual User IdAutorNavigation { get; set; }
        public virtual News IdNewsNavigation { get; set; }
        public virtual Phone IdPhoneNavigation { get; set; }
    }
}
