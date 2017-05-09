using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.News
{
    public class NewsCardViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
        public int CommentCount { get; set; }
        public int ViewsCount { get; set; }
        public string AuthorName { get; set; }
    }
}
