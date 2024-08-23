﻿using System.ComponentModel.DataAnnotations;

namespace KooBits.BookManagement.Validators
{
    public class DateInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date && date > DateTime.Now)
            {
                return new ValidationResult("Published Date cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
