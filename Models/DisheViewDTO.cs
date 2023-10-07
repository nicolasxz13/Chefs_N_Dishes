using System.ComponentModel.DataAnnotations.Schema;
namespace Chefs_N_Dishes.Models
{
    public class DisheViewDTO
    {
        public string Name { get; set; }
        public string ChefName { get; set; }
        public int DisheTastiness { get; set; }
    
        public int DisheCalories { get; set; }
    }
}
