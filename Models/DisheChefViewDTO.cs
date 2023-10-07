using System.ComponentModel.DataAnnotations.Schema;

namespace Chefs_N_Dishes.Models
{
    public class DisheChefViewDTO
    {
        public string Name { get; set; }

        public List<ChefnameDTO> chefnameDTOs;
        public int ChefSelect { get; set; }
        public int Calories { get; set; }
        public int Tastiness { get; set; }
        public string Description { get; set; }
    }

    public class ChefnameDTO
    {
        public int ChefID { get; set; }
        public string Name { get; set; }
    }
}
