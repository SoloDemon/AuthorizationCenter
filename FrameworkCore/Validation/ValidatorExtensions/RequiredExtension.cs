using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FrameworkCore.Validation.ValidatorExtensions
{
    /// <summary>
    ///     空验证
    /// </summary>
    public class RequiredExtension : IValidatorExtensions
    {
        public RequiredExtension()
        {
            AttrType = typeof(RequiredAttribute);
        }

        public void Validator(PropertyInfo property, ValidationContext validationContext,
            List<ValidationResult> validationResults)
        {
            if (property.GetCustomAttribute(typeof(RequiredAttribute), true) is not RequiredAttribute
                validator) return;
            if (!validator.IsValid(property.GetValue(validationContext.ObjectInstance)))
                validationResults.Add(new ValidationResult(validator.ErrorMessage));
        }

        public Type AttrType { get; }
    }
}