using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
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

    // filmes = films
    /// <summary>
    /// Add film to database 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="201">If post works</response>
    [HttpPost("CreateFilm")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AddFilme([FromBody] CreateFilmeDto filmedto) // recebe o parâmetro filme do tipo Filme(classe)
    //o from body serve pro swagger, significa q vamos fzr input de dados lá
    {
        Filme filme = _mapper.Map<Filme>(filmedto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetByIdFilmes), new { id = filme.Id }, filme); //o Created retorna o objeto que criamos no metodo GetById, o new id
    }// é preciso pois no metodo do get temos o parâmetro ID e o ultimo parametro é o objeto que foi criado

    /// <summary>
    /// get filmes with pagination
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">If get works</response>
    /// <response code="400">If get dont work</response>
    [HttpGet("GetFilmsPagination")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetFilmes([FromQuery] int skip = 0, int take = 20) // skip e take funcionam para paginação, o FromQuerry vai vir
    {
        if (skip >= 0 && take >= 0)
        {
            var filmes_pagination = _context.Filmes.Skip(skip).Take(take);
            return (Ok(filmes_pagination));
        }
        else
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// get films by id 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">If get works</response>
    /// <response code="404">If get dont work</response>
    [HttpGet("GetFilmById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByIdFilmes(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }


    /// <summary>
    /// get films by name 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">If get works</response>
    /// <response code="404">If get dont work</response>
    [HttpGet("GetFilmByName/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByTitulo(string name)
    {
        var filme = _context.Filmes.Where(filme => filme.Titulo == name);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

    /// <summary>
    /// updtade films 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">If put works</response>
    /// <response code="404">If trying to edit some object that dont exist in database</response>
    [HttpPut("EditFilm/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto updatefilmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(updatefilmeDto,filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// patch films by id 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">If patch works</response>
    /// <response code="404">If trying to edit some object that dont exist in database</response>
    [HttpPatch("EditParcialFilm/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult EditarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); //pega o id do filme
        if (filme == null) return NotFound();
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeParaAtualizar, ModelState);
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// delete films by id 
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="204">If deletion works</response>
    /// <response code="404">If trying to delete some object that dont exist in database</response>
    [HttpDelete("DeleteById/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult DeleteById(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}