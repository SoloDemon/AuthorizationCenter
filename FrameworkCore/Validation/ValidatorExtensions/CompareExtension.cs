using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FrameworkCore.Validation.ValidatorExtensions
{
    /// <summary>
    ///     对比字符串扩展
    /// </summary>
    public class CompareExtension : IValidatorExtensions
    {
        public CompareExtension()
        {
            AttrType = typeof(CompareAttribute);
        }

        public void Validator(PropertyInfo property, ValidationContext validationContext,
            List<ValidationResult> validationResults)
        {
            if (property.GetCustomAttribute(typeof(CompareAttribute), true) is not CompareAttribute
                validator) return;
            if (!CompareValue(property, validationContext.ObjectInstance, validator.OtherProperty))
                validationResults.Add(new ValidationResult(validator.ErrorMessage));
        }

        public Type AttrType { get; }

        /// <summary>
        ///     对比值
        /// </summary>
        /// <param name="property">属性信息</param>
        /// <param name="instance">验证对象</param>
        /// <param name="otherProperty">对比的属性</param>
        /// <returns></returns>
        private static bool CompareValue(PropertyInfo property, object instance,
            string otherProperty)
        {
            var value = property.GetValue(instance);
            var value1 = instance.GetType().GetProperty(otherProperty)?.GetValue(instance);
            return Equals(value1, value);
        }
    }
}