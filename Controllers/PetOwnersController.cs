using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context) {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
       
         [HttpGet]
        public IEnumerable<PetOwner> GetPetOwners() {
            return _context.PetOwner;
        }
        [HttpGet("{id}")]
        public PetOwner GetByID(int id) {
            return _context.PetOwner.Find(id);
        }
        [HttpPost]
        public IActionResult AddPetOwner(PetOwner petowner) {
            _context.Add(petowner);
            _context.SaveChanges();
            return Ok();
        }
    }
}
