using System;

namespace GsmRanking.Viewmodels.News
{
    public class NewsSlideViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public DateTime PublishDate {get; set; }
    }
}