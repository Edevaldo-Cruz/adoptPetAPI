using adoptPetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace adoptPetAPI.Context
{
    public class AdoptPetContext : DbContext
    {
        public AdoptPetContext()
        {
        }

        public AdoptPetContext(DbContextOptions<AdoptPetContext> options) : base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
