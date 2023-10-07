using System.ComponentModel.DataAnnotations;
using Chefs_N_Dishes.Data;
using Chefs_N_Dishes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chefs_N_Dishes.Controllers
{
    public class Delicious : Controller
    {
        private DeliciousContext _context;

        public Delicious(DeliciousContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            DateTime tempdate = DateTime.Now;
            List<ChefViewDTO> result = _context.Chefs
                .Select(
                    a =>
                        new ChefViewDTO
                        {
                            Name = a.Name,
                            Birthdate = EF.Functions.DateDiffYear(a.Birthdate, tempdate),
                            VNumDishes = a.AllDishes.Count()
                        }
                )
                .ToList();

            return View(result);
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<DisheViewDTO> result = _context.Dishes
                .Include(a => a.Creator)
                .Select(
                    a =>
                        new DisheViewDTO
                        {
                            Name = a.Name,
                            ChefName = a.Creator.Name,
                            DisheTastiness = a.Tastiness,
                            DisheCalories = a.Calories
                        }
                )
                .ToList();
            return View(result);
        }

        [HttpGet("dishes/new")]
        public IActionResult New()
        {
            DisheChefViewDTO disheChefViewDTO = new DisheChefViewDTO();
            ChefListPopulate(disheChefViewDTO);
            return View(disheChefViewDTO);
        }

        [HttpPost("dishes/create")]
        public IActionResult Create(DisheChefViewDTO disheChefViewDTO)
        {
            Dishe dishe = new Dishe();
            //Need to refactor code.
            if (disheChefViewDTO.ChefSelect != null)
            {
                var result = _context.Chefs.SingleOrDefault(
                    a => a.ChefId == disheChefViewDTO.ChefSelect
                );

                if (result == null)
                {
                    ModelState.AddModelError("ChefSelect", "Chef select is not valid!");
                    ChefListPopulate(disheChefViewDTO);
                    return View("New", disheChefViewDTO);
                }
                dishe.Creator = result;
            }
            else
            {
                ModelState.AddModelError("ChefSelect", "Chef select is not valid!");
                ChefListPopulate(disheChefViewDTO);
                return View("New", disheChefViewDTO);
            }

            dishe.Name = disheChefViewDTO.Name;
            dishe.Tastiness = disheChefViewDTO.Tastiness;
            dishe.Calories = disheChefViewDTO.Calories;
            dishe.Description = disheChefViewDTO.Description;

            List<ValidationResult> validationResults = new();
            ValidationContext validationContext = new(dishe);

            bool isValid = Validator.TryValidateObject(
                dishe,
                validationContext,
                validationResults,
                true
            );

            if (isValid)
            {
                dishe.CreatedAt = DateTime.Now;
                dishe.UpdatedAt = DateTime.Now;
                _context.Add(dishe);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ChefListPopulate(disheChefViewDTO);
                return View("New", disheChefViewDTO);
            }
        }

        private void ChefListPopulate(DisheChefViewDTO disheChefViewDTO)
        {
            disheChefViewDTO.chefnameDTOs = _context.Chefs
                .Select(a => new ChefnameDTO { Name = a.Name, ChefID = a.ChefId })
                .ToList();
        }
    }
}
