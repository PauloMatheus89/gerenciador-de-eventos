using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GerenciadorEventos.CustomValidations
{
    public class DateRangeValidationAttribute : ValidationAttribute
    {
        public string? PropertyName { get; set; }

        public readonly string NullDefaultMessage = "{0} cannot be null";
        

        public DateRangeValidationAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime startingDate = Convert.ToDateTime(value);

                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(PropertyName!);
                string? otherPropertyName = otherProperty?.Name;

                if (otherProperty != null)
                {
                    DateTime endDate = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                    if (startingDate < endDate)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage, [PropertyName, validationContext.MemberName]);
                    }
                }
                else
                {
                    return new ValidationResult(string.Format(NullDefaultMessage,otherPropertyName));
                }
            }
            else
            {                                                               //Member name retorna o nome da propriedade de onde value obtem seu valor
                return new ValidationResult(string.Format(NullDefaultMessage,validationContext.MemberName));
            }
        }
    }

}