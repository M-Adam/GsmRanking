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

        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Krótki opis")]
        public string Shorttext { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Display(Name = "Zdjęcie newsa")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }
    }
}
