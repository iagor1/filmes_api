using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    
    private static List<Filme> filmes = new List<Filme>(); // criou uma lista da classe Filme

    [HttpPost]
    public void AddFilme([FromBody]Filme filme) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        filmes.Add(filme); // add o filme na lista, o método .Add() é um método de List(lista genérica)
    }

}