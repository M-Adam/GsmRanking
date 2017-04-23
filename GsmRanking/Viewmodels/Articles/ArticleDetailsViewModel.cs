using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.Articles
{
    public class ArticleDetailsViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string ShortText { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public int ViewsCount { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate { get; set; }

        //Todo: add comments
    }
}
