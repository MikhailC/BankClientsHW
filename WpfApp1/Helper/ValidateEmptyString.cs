using System;
using System.Globalization;
using System.Windows.Controls;

namespace WpfApp1.Helper
{
    public class ValidateEmptyString:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (String.IsNullOrEmpty(value as string) || String.IsNullOrWhiteSpace(value as string))
            {
                return new ValidationResult(false, "Should be filled");
            }
            return ValidationResult.ValidResult;
        }
        
    }
}