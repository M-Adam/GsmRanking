using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GsmRanking.Viewmodels.Articles
{
    public class ArticleCreateViewModel
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortText { get; set; }
        [Required]
        [MinLength(50)]
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime CreateDate { get; set; }

        
    }
}
