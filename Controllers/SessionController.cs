using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : Controller
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessionController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult AdicionaSessao(CreateSessionDto dto)
        {
            Session session = _mapper.Map<Session>(dto);
            _context.Session.Add(session);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = session.Id }, session);
        }

        [HttpGet]
        public IEnumerable<ReadSessionDto> RecuperaSessoes()
        {
            return _mapper.Map<List<ReadSessionDto>>(_context.Session.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaSessoesPorId(int id)
        {
            Session sessao = _context.Session.FirstOrDefault(sessao => sessao.Id == id);
            if (sessao != null)
            {
                ReadSessionDto sessaoDto = _mapper.Map<ReadSessionDto>(sessao);

                return Ok(sessaoDto);
            }
            return NotFound();
        }
    
}
}
