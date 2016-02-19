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

        public CheckDuplicates(params string[] propertyNames) : base("{0} choice should be unique" )
        {
            this.PropertyNames = propertyNames;
        
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
                var values = properties.Select(p => p.GetValue(validationContext.ObjectInstance, null)).OfType<int>();

                int[] id = { (int) value, values.ElementAt(0), values.ElementAt(1), values.ElementAt(2) };

                if ( id[0] == id[1] ||
                     id[0] == id[2] ||
                     id[0] == id[3])
                {
                    return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}
