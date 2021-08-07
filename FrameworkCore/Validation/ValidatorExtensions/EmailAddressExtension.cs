using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FrameworkCore.Validation.ValidatorExtensions
{
    /// <summary>
    ///     Email地址扩展
    /// </summary>
    public class EmailAddressExtension : IValidatorExtensions
    {
        public EmailAddressExtension()
        {
            AttrType = typeof(EmailAddressAttribute);
        }

        public void Validator(PropertyInfo property, ValidationContext validationContext,
            List<ValidationResult> validationResults)
        {
            if (property.GetCustomAttribute(typeof(EmailAddressAttribute), true) is not EmailAddressAttribute
                validator) return;
            if (!validator.IsValid(property.GetValue(validationContext.ObjectInstance)))
                validationResults.Add(new ValidationResult(validator.ErrorMessage));
        }

        public Type AttrType { get; }
    }
}