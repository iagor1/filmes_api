namespace FilmesApi.Models;
using System.ComponentModel.DataAnnotations;

public class Filme
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage ="Campo titulo vazio !")]
    public string Titulo {get;set;}
    [Required(ErrorMessage ="Campo gênero vazio !")]
    [MaxLength(60,ErrorMessage ="Tamanho do titulo excedido")]
    public string Genero {get;set;}   
    [Required]
    [Range(70,600,ErrorMessage ="Duração deve ser entre 70 e 600")]
    public int Duracao {get;set;}

    public virtual ICollection<Session> Sessions { get; set; }
}