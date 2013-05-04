using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Web.Models.Test
{
    public class ChangeNameDescModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public long Id { get; set; }
        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        /// <param name="validationContext">The validation context.</param>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(this.Name))
                yield return new ValidationResult("Введите название!", new[] { "Name" });
            if (String.IsNullOrWhiteSpace(this.Description))
                yield return new ValidationResult("Введите описание!", new[] { "Description" });

        }
    }
}