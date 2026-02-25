using System.ComponentModel.DataAnnotations;

namespace CBS.Client.Helper
{
    public static class InputValidator
    { // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        /// <summary>
        /// Validates any model that uses DataAnnotations attributes
        /// (e.g. [Required], [EmailAddress], [MinLength] on your model classes).
        /// </summary>
        public static IEnumerable<string> ValidateModel(object model)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(model, context, results, validateAllProperties: true);

            return results.Select(r => r.ErrorMessage ?? "Validation error.");
        }
    }
}
