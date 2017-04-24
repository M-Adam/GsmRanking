using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GsmRanking.Viewmodels.News
{
    public class NewsEditViewModel
    {
        public int IdNews { get; set; }
        public string Title { get; set; }
        public string Shorttext { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Display(Name = "Is published?")]
        public bool Ispublished { get; set; }
        [Display(Name = "Upload image")]
        public IFormFile ImageUpload { get; set; }
        public string Image { get; set; }
    }
}
