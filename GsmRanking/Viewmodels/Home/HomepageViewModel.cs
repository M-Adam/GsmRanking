using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GsmRanking.Viewmodels.Articles;
using GsmRanking.Viewmodels.Phones;

namespace GsmRanking.Viewmodels.Home
{
    public class HomepageViewModel
    {
        public ArticlesViewModel NewestArticles { get; set; }
        public NewestPhonesViewModel NewestPhones { get; set; }
    }
}
