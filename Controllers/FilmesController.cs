using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    public FilmesController(FilmeContext context)
    {
        _context = context;
    }


    private FilmeContext _context;

    [HttpPost]
    public IActionResult AddFilme([FromBody]Filme filme) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetByIdFilmes), new {id = filme.Id}, filme); //o Created retorna o objeto que criamos no metodo GetById, o new id
    }// é preciso pois no metodo do get temos o parâmetro ID e o ultimo parametro é o objeto que foi criado

    [HttpGet]
    public IEnumerable<Filme> GetFilmes([FromQuery]int skip = 0, int take = 20) // skip e take funcionam para paginação, o FromQuerry vai vir
    {
        return _context.Filmes.Skip(skip).Take(take);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetByIdFilmes(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
    [HttpGet("titulos/{name}")]
    public IActionResult GetByTitulo(string name)
    {
        var filme = _context.Filmes.Where(filme => filme.Titulo == name);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
}