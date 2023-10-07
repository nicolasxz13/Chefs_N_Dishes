using System.ComponentModel.DataAnnotations.Schema;
namespace Chefs_N_Dishes.Models
{
    
    public class ChefViewDTO
    {
        public string Name { get; set; }
        public int? Birthdate { get; set; }
        public int VNumDishes { get; set; }
    }
}
