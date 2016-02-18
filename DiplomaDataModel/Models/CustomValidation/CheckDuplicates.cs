using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Models.CustomValidation
{
    public class CheckDuplicates : ValidationAttribute
    {
        public string[] PropertyNames { get; private set; }

        public CheckDuplicates(params string[] propertyNames) : base("{0} has duplicate option" )
        {
            this.PropertyNames = propertyNames;
        
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
                var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<int>();

                int[] id = { values.ElementAt(0), values.ElementAt(1), values.ElementAt(2) };

                if (id.Distinct().Count() != id.Count())
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
               
            }

            return ValidationResult.Success;
        }
    }
}
