using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Comments
    {
        public int IdComment { get; set; }
        public int IdAutor { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public int? IdNews { get; set; }
        public int? IdArticle { get; set; }
        public int? IdPhone { get; set; }

        public virtual Articles IdArticleNavigation { get; set; }
        public virtual Users IdAutorNavigation { get; set; }
        public virtual News IdNewsNavigation { get; set; }
        public virtual Phones IdPhoneNavigation { get; set; }
    }
}
