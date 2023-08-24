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
    public void AddFilme([FromBody]Filme filme) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        filme.Id = id++;
        filmes.Add(filme); // add o filme na lista, o método .Add() é um método de List(lista genérica)
    }

    [HttpGet]
    public IEnumerable<Filme> GetFilmes()
    {
        return filmes;
    }
    
    [HttpGet("{id}")]
    public Filme? GetByIdFilmes(int id)
    {
        return filmes.FirstOrDefault(filme => filme.Id == id);
    }
    [HttpGet("titulos/{name}")]
    public Filme? GetByNames(string name)
    {
        return filmes.Find(filme => filme.Titulo == name);
    }
}