using System.ComponentModel.DataAnnotations;

namespace adoptPetAPI.Models
{
    public class User
    {
        [Key]
        public int IdUser {get; set; }
        [Required]
        [MaxLength(150)]
        public string Name {get; set; }
        [Required]
        [MaxLength(250)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        public string Telephone { get; set; }
        [Required]
        [MaxLength(250)]
        public string Address { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
