using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetPets() {
            return _context.Pet.Include(pet => pet.PetOwner);
        }
        [HttpGet("{id}")]
        public Pet GetByID(int id) {
            return _context.Pet.Find(id);
        }
        [HttpPost]
        public IActionResult AddPet(Pet pet) {
            
            _context.Add(pet);
            _context.SaveChanges();
            return CreatedAtAction(nameof(AddPet), new {id = pet.id}, pet);
        }
        [HttpPut("{id}")]
        public Pet UpdatePet(Pet pet, int id) {
            pet.id = id;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
        [HttpPut("{id}/checkin")]
        public Pet UpdateCheckin(int id) {
            Pet pet = GetByID(id);
            pet.CheckedInAt = DateTime.Now;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
        [HttpPut("{id}/checkout")]
        public Pet UpdateCheckout(int id) {
            Pet pet = GetByID(id);
            pet.CheckedInAt = null;
            _context.Update(pet);
            _context.SaveChanges();
            return pet;
        }
        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id) {
            Pet pet = GetByID(id);
            _context.Pet.Remove(pet);
            _context.SaveChanges();
            return NoContent();
        }
        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
