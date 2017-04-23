using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class Articles
    {
        public Articles()
        {
            Comments = new HashSet<Comments>();
        }

        public int IdArticle { get; set; }
        public int IdAutor { get; set; }
        public string ShortText { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual Users IdAutorNavigation { get; set; }
    }
}
