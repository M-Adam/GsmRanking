using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.News
{
    public class NewsCardViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
