using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.DataAnnotations
{
    public class FullNameValidationAttribute: ValidationAttribute
    {
        private readonly string _regexPattern;
        public FullNameValidationAttribute()
        {
            _regexPattern = @"^([a-zA-Z,/.-]+)\s([a-zA-Z,/.-]+)$";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string fullName = value.ToString();

            if (!Regex.IsMatch(fullName, _regexPattern))
            {
                return new ValidationResult(ErrorMessage ?? "Invalid full name format");
            }

            return ValidationResult.Success;
        }
    }
}
