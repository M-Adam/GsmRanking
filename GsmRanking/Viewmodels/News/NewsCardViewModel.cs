﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.News
{
    public class NewsCardViewModel
    {
        public IList<NewsListCardViewModel> News { get; set; }
    }
}