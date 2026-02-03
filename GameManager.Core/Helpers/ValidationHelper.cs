using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        public static void ModelValidation(object obj)
        {
            // Run DataAnnotations validation
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(obj);

            if (!Validator.TryValidateObject(obj, context, validationResults, true))
            {
                // Pick the first error or aggregate them
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
