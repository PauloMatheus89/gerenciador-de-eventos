using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GerenciadorEventos.CustomValidations
{
    public class DocumentValidation : ValidationAttribute
    {

        public string CPFErrorMessage { get; set; } = "{0} is not a valid CPF!";
        public string CNPJErrorMessage { get; set; } = "{0} is not a valid CNPJ";

        public string GeneralErrorMessage { get; set; } = "{0} is not a valid CNPJ or CNPJ";

        public string NullErrorMessage { get; set; } = "{0} cannot be null";

        public const string cpfGRegex = @"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$";
        public const string cnpjRegex = @"^\d{2}\.?\d{3}\.?\d{3}/?\d{4}-?\d{2}$";

        protected override ValidationResult? IsValid(object? value,ValidationContext validationContext)
        {
            string? document = value as string;

            if (!string.IsNullOrWhiteSpace(document))
            {
                if (document.Length == 11)
                {
                    if (Regex.IsMatch(document, cpfGRegex))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(string.Format(ErrorMessage ?? CPFErrorMessage, document));
                    }
                }
                else if (document.Length == 14)
                {
                    if (Regex.IsMatch(document, cnpjRegex))
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(string.Format(ErrorMessage ?? CNPJErrorMessage, document));
                    }
                }
                else
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? GeneralErrorMessage, document));
                }
            }
            else
            {
                return new ValidationResult(string.Format(ErrorMessage ?? NullErrorMessage, document));
            }
        }
    }
}