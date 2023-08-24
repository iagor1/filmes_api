using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    
    private static List<Filme> filmes = new List<Filme>(); // criou uma lista da classe Filme
    private static int id=0;
    [HttpPost]
    public IActionResult AddFilme([FromBody]Filme filme) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        filme.Id = id++;
        filmes.Add(filme); // add o filme na lista, o método .Add() é um método de List(lista genérica)
        return CreatedAtAction(nameof(GetByIdFilmes), new {id = filme.Id}, filme); //o Created retorna o objeto que criamos no metodo GetById, o new id
    }// é preciso pois no metodo do get temos o parâmetro ID e o ultimo parametro é o objeto que foi criado

    [HttpGet]
    public IEnumerable<Filme> GetFilmes([FromQuery]int skip = 0, int take = 20) // skip e take funcionam para paginação, o FromQuerry vai vir
    {
        return filmes.Skip(skip).Take(take);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetByIdFilmes(int id)
    {
        var filme = filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok();
    }
    [HttpGet("titulos/{name}")]
    public IActionResult GetByNames(string name)
    {
        var filme = filmes.Find(filme => filme.Titulo == name);
        if (filme == null) return NotFound();
        return Ok();
    }
}