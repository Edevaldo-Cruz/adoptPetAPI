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

        //[HttpPost]
        //public IActionResult Create(Pet pet)
        //{
        //    _context.Add(pet);
        //    _context.SaveChanges();
        //    return Ok(pet);
        //}

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
                var userResponse = await httpClient.GetStringAsync("https://randomuser.me/api/?nat=br");
                var userData = JsonConvert.DeserializeObject<UserApiResponse>(userResponse);
                var userFirstName = userData.results[0].name.first;

                // Criar novo pet
                var newPet = new Pet
                {
                    Name = userFirstName,
                    Image = dogPhotoUrl
                };
                _context.Add(newPet);
                _context.SaveChanges();
                return Ok(newPet);
            }
        }


        //[HttpPost("AddIfNeeded")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> AddPetsIfNeeded()
        //{
        //    using (var context = new AdoptPetContext())
        //    {
        //        int currentCount = context.Pets.Count(p => !string.IsNullOrEmpty(p.Human));
        //        if (currentCount < 10)
        //        {
        //            var httpClient = new HttpClient();

        //            // Obter foto aleatória de cachorro
        //            var dogResponse = await httpClient.GetStringAsync("https://dog.ceo/api/breeds/image/random");
        //            var dogData = JsonConvert.DeserializeObject<DogApiResponse>(dogResponse);
        //            var dogPhotoUrl = dogData.message;

        //            // Obter nome aleatório
        //            var userResponse = await httpClient.GetStringAsync("https://randomuser.me/api/?nat=br");
        //            var userData = JsonConvert.DeserializeObject<UserApiResponse>(userResponse);
        //            var userFirstName = userData.results[0].name.first;

        //            // Criar novo pet
        //            var newPet = new Pet
        //            {
        //                Name = userFirstName,
        //                Image = dogPhotoUrl
        //            };
        //            context.Pets.Add(newPet);
        //            await context.SaveChangesAsync();
        //        }
        //    }

        //    return NoContent();
        //}


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
