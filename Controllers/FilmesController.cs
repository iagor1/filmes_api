using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmesController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;
    public FilmesController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddFilme([FromBody]CreateFilmeDto filmedto) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        Filme filme = _mapper.Map<Filme>(filmedto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetByIdFilmes), new {id = filme.Id}, filme); //o Created retorna o objeto que criamos no metodo GetById, o new id
    }// é preciso pois no metodo do get temos o parâmetro ID e o ultimo parametro é o objeto que foi criado

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)] 
    public IActionResult GetFilmes([FromQuery]int skip = 0, int take = 20) // skip e take funcionam para paginação, o FromQuerry vai vir
    {
        var filmes_pagination = _context.Filmes.Skip(skip).Take(take);
        return(Ok(filmes_pagination));
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetByIdFilmes(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
    [HttpGet("titulos/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetByTitulo(string name)
    {
        var filme = _context.Filmes.Where(filme => filme.Titulo == name);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
    /// <summary>
    /// Delete film by ID
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteById(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}