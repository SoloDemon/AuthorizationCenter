using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FrameworkCore.Validation.ValidatorExtensions
{
    public interface IValidatorExtensions
    {
        Type AttrType { get; }

        /// <summary>
        ///     验证器
        /// </summary>
        /// <param name="property"></param>
        /// <param name="validationContext"></param>
        /// <param name="validationResults"></param>
        /// <returns></returns>
        void Validator(PropertyInfo property, ValidationContext validationContext,
            List<ValidationResult> validationResults);
    }
}