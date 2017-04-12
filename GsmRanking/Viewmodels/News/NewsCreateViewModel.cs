using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.News
{
    public class NewsCreateViewModel
    {
        public string Title { get; set; }
        public string Shorttext { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime Createdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Publishdate { get; set; }
        [Display(Name = "Is published?")]
        public bool Ispublished { get; set; }
        [Display(Name = "Upload image")]
        public IFormFile ImageUpload { get; set; }
    }
}
