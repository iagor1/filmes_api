using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Place { get; set; }
        public int Number { get; set; }
        public virtual Cinema Cinema { get; set; }

    }
}
