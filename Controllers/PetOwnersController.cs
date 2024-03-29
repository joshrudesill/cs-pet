using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetOwner>>> GetPetOwners()
        {
            var ownersWithPetCounts = await _context.PetOwner
            .Select(owner => new PetOwner
            {
                id = owner.id,
                EmailAddress = owner.EmailAddress,
                name = owner.name,
                PetCount = _context.Pet.Count(pet => pet.PetOwnerId == owner.id) // Count of Pets for each owner
            })
            .ToListAsync();
             
            return Ok(ownersWithPetCounts);
        }
        [HttpGet("{id}")]
        public PetOwner GetByID(int id)
        {
            return _context.PetOwner.Find(id);
        }
        [HttpPost]
        public IActionResult AddPetOwner(PetOwner petowner)
        {
            _context.Add(petowner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(AddPetOwner), new {id = petowner.id}, petowner);
        }

        [HttpPut("{id}")]
        public PetOwner Put(int id, PetOwner petOwner)
        {
            // Our DB context needs to know the id of the bread to update
            petOwner.id = id;

            // Tell the DB context about our updated bread object
            _context.Update(petOwner);

            // ...and save the bread object to the database
            _context.SaveChanges();

            // Respond back with the created bread object
            return petOwner;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Find the bread, by ID
            PetOwner petOwner = _context.PetOwner.Find(id);

            // Tell the DB that we want to remove this bread
            _context.PetOwner.Remove(petOwner);

            // ...and save the changes to the database
            _context.SaveChanges(); 
            return NoContent();
        }
    }
}
