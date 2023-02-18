using adoptPetAPI.Context;
using adoptPetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace adoptPetAPI.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UserController : Controller
    {
        private readonly AdoptPetContext _context;

        public UserController(AdoptPetContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var urses = _context.Users;
            return Ok(urses);
        }

        [HttpGet("GetUserById")]
        public IActionResult GetById(int id)
        {
            var user = _context.Users.Find(id);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("GetUserByName")]
        public IActionResult GetByName(string name)
        {
            var user = _context.Users.Where(x => x.Name.Contains(name));
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, User user)
        {
            var userBank = _context.Users.Find(id);

            if (user == null)
                return NotFound();

            userBank.Name = user.Name;
            userBank.DateOfBirth = user.DateOfBirth;
            userBank.Password = user.Password;
            userBank.Email = user.Email;
            userBank.Address = user.Address;
            userBank.Telephone = user.Telephone;

            _context.Users.Update(userBank);
            _context.SaveChanges();

            return Ok(userBank);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userBank = _context.Users.Find(id);

            if (userBank == null)
                return NotFound();

            _context.Users.Remove(userBank);
            _context.SaveChanges();

            return NoContent();
        }


    }
}

