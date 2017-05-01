using System;
using System.Collections.Generic;

namespace GsmRanking.Models
{
    public partial class News
    {
        public News()
        {
            Comments = new HashSet<Comment>();
        }

        public int IdNews { get; set; }
        public int IdAutor { get; set; }
        public string ShortText { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public bool IsPublished { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
