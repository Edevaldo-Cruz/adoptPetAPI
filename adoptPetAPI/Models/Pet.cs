using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace adoptPetAPI.Models
{
    public class Pet
    {
        [Key]
        public int IdPet { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
        public int? IdUser { get; set; }

        [ForeignKey("IdUser")]
        public User? User { get; set; }
    }
}
