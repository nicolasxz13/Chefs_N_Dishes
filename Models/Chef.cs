using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefs_N_Dishes.Models
{
    public class Chef
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChefId { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [FutureDate]
        [LegalDate]
        public DateTime? Birthdate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Dishe> AllDishes { get; set; } = new List<Dishe>();
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime inputDate = (DateTime)value;
            DateTime dateTimeNow = DateTime.Now;

            if (inputDate > dateTimeNow)
            {
                return new ValidationResult("The date must be a future date.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

    public class LegalDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext
        )
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime inputDate = (DateTime)value;
            DateTime dateTimeNow = DateTime.Now;

            if ((inputDate.Year - dateTimeNow.Year) > 18)
            {
                return new ValidationResult("Does not meet age requirements.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
