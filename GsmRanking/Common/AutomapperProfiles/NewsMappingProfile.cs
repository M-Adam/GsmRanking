using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GsmRanking.Viewmodels.News;
using GsmRanking.Models;

namespace GsmRanking.Common.AutomapperProfiles
{
    public class NewsMappingProfile : Profile
    {
        public NewsMappingProfile()
        {
            CreateMap<News, NewsCreateViewModel>();
            CreateMap<NewsCreateViewModel, News>();

            CreateMap<News, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, News>().ForMember(m => m.CreateDate, opt => opt.Ignore())
                                                .ForMember(m=>m.Image, opt => opt.Ignore());
        }
    }
}
