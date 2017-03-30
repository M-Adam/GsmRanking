using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GsmRanking.Common;
using Microsoft.AspNetCore.Mvc;

namespace GsmRanking.Viewmodels.Account
{
    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nie podano loginu.")]
        [DataType(DataType.Text)]
        [MinLength(3, ErrorMessage = "Login musi mieć przynajmniej 3 znaki.")]
        [MaxLength(100, ErrorMessage = "Login może mieć maksymalnie 100 znaków.")]
        [Remote(action: "VerifyUsername", controller: "Validation", HttpMethod = "Get", ErrorMessage = "Login zajęty.")]
        [Display(Description = "Login")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Nie podano adresu e-mail.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Adres e-mail ma niepoprawny format.")]
        [Remote(action: "VerifyEmail", controller: "Validation", HttpMethod = "Get", ErrorMessage = "Na podany adres zostało już zarejestrowane konto.")]
        [Display(Description = "Adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nie podano hasła.")]
        [Password]
        [DataType(DataType.Password)]
        [Display(Description = "Hasło")]
        public string Password { get; set; }
    }
}
