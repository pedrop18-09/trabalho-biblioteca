using Microsoft.AspNetCore.Mvc;
using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;

namespace trabalho_biblioteca.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] AutorRequest request)
        {
            try
            {
                var autor = _autorService.CadastrarAutor(request);
                return CreatedAtAction(nameof(ObterAutorPorId), new { id = autor.Id }, autor);
            }

            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var autores = _autorService.ListarAutores();
            return Ok(autores);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterAutorPorId(int id)
        {
            try
            {
                var autor = _autorService.ObterAutorPorId(id);
                return Ok(autor);
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizarAutor(int id, [FromBody] AutorRequest request)
        {
            try
            {
                var autorAtualizado = _autorService.AtualizarAutor(id, request);
                return Ok(autorAtualizado);
            }
            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletarAutor(int id)
        {
            try
            {
                _autorService.DeletarAutor(id);
                return NoContent();
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
