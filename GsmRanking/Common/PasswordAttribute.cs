using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GsmRanking.Common
{
    public class PasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = new ValidationResult(string.Empty);
            var password = value as string;

            var message = PasswordCheck(password);

            if (!string.IsNullOrEmpty(message))
            {
                result.ErrorMessage = message;
                return result;
            }

            return ValidationResult.Success;
        }

        public override bool IsValid(object value)
        {
            return !string.IsNullOrEmpty(PasswordCheck(value as string));
        }

        private string PasswordCheck(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Podano puste hasło.";
            }

            var messageBuilder = new StringBuilder();
            if (password.Length < 9)
            {
                messageBuilder.AppendLine($"Hasło jest za krótkie. Hasło musi mieć przynajmniej 9 znaków.");
            }
            if (!password.Any(char.IsUpper))
            {
                messageBuilder.AppendLine("Wymagana jest przynajmniej jedna wielka litera.");
            }
            if (!password.Any(char.IsDigit))
            {
                messageBuilder.AppendLine("Wymagana jest przynajmniej jedna cyfra.");
            }
            if (!Regex.IsMatch(password, @"([`~!@#$%^&*()_=+[{};:',<>/?]){1,}"))
            {
                messageBuilder.AppendLine("Wymagany jest przynajmniej jeden symbol.");
            }

            return messageBuilder.ToString();
        }
    }
}
