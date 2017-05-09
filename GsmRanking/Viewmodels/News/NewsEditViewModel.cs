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
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Dodaj krótki opis newsa")]
        [Display(Name = "Krótki opis")]
        public string Shorttext { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Dodaj treść newsa")]
        public string Content { get; set; }

        [Display(Name = "Zdjęcie newsa")]
        public IFormFile ImageUpload { get; set; }

        public string Image { get; set; }

        public string AuthorName { get; set; }
    }
}
