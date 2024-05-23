using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Data.CustomValidationeRules
{
    public class MinWordsAttribute : ValidationAttribute
    {
        public int MinWords { get; set; }
        public MinWordsAttribute() { }
        public MinWordsAttribute(int MinWords)
        {
            this.MinWords = MinWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null)
            {
                string fieldValue = (string)value;
                string[] words = fieldValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (words.Length < MinWords)
                {
                    return new ValidationResult($"La descrizione deve contenere almeno {MinWords} parole");
                }
            }

            return ValidationResult.Success;
        } 
    }
}
