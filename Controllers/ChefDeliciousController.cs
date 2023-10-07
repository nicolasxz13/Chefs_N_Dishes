using Chefs_N_Dishes.Data;
using Chefs_N_Dishes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Chefs_N_Dishes.Controllers
{
    public class ChefDelicious : Controller
    {
        private DeliciousContext _context;

        public ChefDelicious(DeliciousContext context)
        {
            _context = context;
        }

        [HttpGet("chefs/new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("chefs/create")]
        public IActionResult Create(Chef chef)
        {
            if (ModelState.IsValid)
            {
                chef.CreatedAt = DateTime.Now;
                chef.UpdatedAt = DateTime.Now;
                _context.Add(chef);
                _context.SaveChanges();
                return RedirectToAction("Index", "Delicious");
            }else{
                return View("New");
            }
        }
    }
}
