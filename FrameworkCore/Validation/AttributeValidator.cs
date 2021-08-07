using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FrameworkCore.Helper;
using FrameworkCore.Validation.ValidatorExtensions;

namespace FrameworkCore.Validation
{
    /// <summary>
    ///     验证特性
    /// </summary>
    public static class AttributeValidator
    {
        /// <summary>
        ///     验证器
        /// </summary>
        /// <param name="instance">实例</param>
        /// <param name="validationContext">验证上下文</param>
        /// <param name="results">结果</param>
        /// <returns></returns>
        public static bool Validator(object instance, ValidationContext validationContext,
            List<ValidationResult> results)
        {
            var properties = instance.GetType().GetRuntimeProperties()
                .Where(IsPublic); //&& x.GetIndexParameters().Any()
            foreach (var property in properties)
                try
                {
                    ValidatorProperty(property, validationContext, results);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            return !results.Any();
        }

        /// <summary>
        ///     验证属性
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="validationContext">验证上下文</param>
        /// <param name="validationResults">验证结果</param>
        /// <returns></returns>
        private static void ValidatorProperty(PropertyInfo property,
            ValidationContext validationContext, List<ValidationResult> validationResults)
        {
            var validatorExtensions = ReflectionHelper.CreateAllInstancesOf<IValidatorExtensions>();
            var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), true);
            foreach (var attribute in attributes)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var validator = validatorExtensions.FirstOrDefault(x => x.AttrType == attribute.GetType());
                validator?.Validator(property, validationContext, validationResults);
            }
        }

        private static bool IsPublic(PropertyInfo property)
        {
            return property.GetMethod != null && property.GetMethod.IsPublic ||
                   property.SetMethod != null && property.SetMethod.IsPublic;
        }
    }
}