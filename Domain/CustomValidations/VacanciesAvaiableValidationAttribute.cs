using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GerenciadorEventos.CustomValidations
{
    public class VacanciesAvaiableValidationAttribute : ValidationAttribute
    {
        public string? PropertyName { get; set; }

        public readonly string TotalNumberErrorMessage = "Avaiable Vacancies shoud be less or equal to total vacancies";
        public readonly string NullErrorMessage = "{0} cannot be null";

        public VacanciesAvaiableValidationAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int avaiableVacancies = Convert.ToInt32(value);

                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(PropertyName!);

                if (otherProperty != null)
                {
                    int totalVacancies = Convert.ToInt32(otherProperty.GetValue(validationContext.ObjectInstance));

                    if (avaiableVacancies > totalVacancies)
                    {
                        return new ValidationResult(TotalNumberErrorMessage, [PropertyName!, validationContext.MemberName!]);
                    }

                    return ValidationResult.Success;

                }

                return new ValidationResult($"{PropertyName} cannot be null");

            }
            
            return new ValidationResult(NullErrorMessage, [validationContext.MemberName!]);
        }
    }
}