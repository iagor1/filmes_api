using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, ErrorMessage = "Titulo muito grande")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O campo é obrigatório")]
        [StringLength(50, ErrorMessage = "Genero muito grande")]
        public string Genero { get; set; }
        [Required]
        [Range(70, 600, ErrorMessage = "Duração muito grande...")]
        public int Duracao { get; set; }
    }
}
