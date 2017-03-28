using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.Articles
{
    public class ArticleListViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Comments { get; set; }
    }
}
