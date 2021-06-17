using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCSchool.Models.Validations
{
    public class CourseDatesVal : ValidationAttribute, IClientValidatable
    {
        private const string DefaultErrorMessage = "{0} must be on or after Start Date";

        public CourseDatesVal()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(DefaultErrorMessage, name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Course course = new Course();
            var dateEntered = (DateTime)value;
            if (dateEntered < course.StartDate)
            {
                var message = FormatErrorMessage(dateEntered.ToShortDateString());
                return new ValidationResult(message);
            }
            return null;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientCustomDateValidationRule(FormatErrorMessage(metadata.DisplayName));
            yield return rule;
        }
    }

    public sealed class ModelClientCustomDateValidationRule : ModelClientValidationRule
    {

        public ModelClientCustomDateValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "datemustbeequalorgreaterthanstartdate";
        }
    }
}
