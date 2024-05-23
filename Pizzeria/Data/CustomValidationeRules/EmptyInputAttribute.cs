using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Data.CustomValidationeRules
{
    public class EmptyInputAttribute : ValidationAttribute
    {
        public EmptyInputAttribute() { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (Convert.ToInt32(value) == 0 || value == null || value == " ")
            {
                return new ValidationResult($"Input non puo essere vuoto");
            }

            return ValidationResult.Success;
        }
    }
}
