using adoptPetAPI.Context;
using adoptPetAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace adoptPetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : Controller
    {
        private readonly AdoptPetContext _context;

        public PetController(AdoptPetContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            using (var httpClient = new HttpClient())
            {
                // Obter foto aleatória de cachorro
                var dogResponse = await httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");
                var dogData = JsonConvert.DeserializeObject<DogApiResponse>(dogResponse);
                var dogPhotoUrl = dogData.message;

                // Obter nome aleatório
                var PetResponse = await httpClient.GetStringAsync("https://randomuser.me/api/?nat=br");
                var PetData = JsonConvert.DeserializeObject<PetApiResponse>(PetResponse);
                var PetFirstName = PetData.results[0].name.first;

                // Criar novo pet
                var newPet = new Pet
                {
                    Name = PetFirstName,
                    Image = dogPhotoUrl
                };
                _context.Add(newPet);
                _context.SaveChanges();
                return Ok(newPet);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, Pet pet)
        {
            var petBank = _context.Pets.Find(id);

            if (petBank == null)
                return NotFound();

            petBank.Name = pet.Name;
            petBank.Image = pet.Image;
            petBank.IdUser = pet.IdUser;
            petBank.Description = pet.Description;

            _context.Pets.Update(petBank);
            _context.SaveChanges();

            return Ok(petBank);
        }

        [HttpGet("GetAllPets")]
        public IActionResult GetAllPets()
        {
            var pets = _context.Pets;
            return Ok(pets);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var petBank = _context.Pets.Find(id);

            if (petBank == null)
                return NotFound();

            _context.Pets.Remove(petBank);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
