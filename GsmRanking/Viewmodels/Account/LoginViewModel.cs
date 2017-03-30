using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GsmRanking.Viewmodels.Account
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Description = "Login")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Description = "Hasło")]
        public string Password { get; set; }

        [Display(Description = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}
