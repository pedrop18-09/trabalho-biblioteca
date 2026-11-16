using Microsoft.AspNetCore.Mvc;
using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;

namespace trabalho_biblioteca.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] CriarLivroRequest request)
        {
            try
            {
                var livro = _livroService.CadastrarLivro(request);
                return CreatedAtAction(nameof(ObterLivroPorId), new { id = livro.Id }, livro);
            }

            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }

            catch (ConflitoException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Listar([FromQuery] int? autorId)
        {
            var livros = _livroService.ListarLivros(autorId);
            return Ok(livros);
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterLivroPorId(int id)
        {
            try
            {
                var livro = _livroService.ObterLivroPorId(id);
                return Ok(livro);
            }

            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult AtualizarLivro(int id, [FromBody] CriarLivroRequest request)
        {
            try
            {
                var livroAtualizado = _livroService.AtualizarLivro(id, request);
                return Ok(livroAtualizado);
            }

            catch (ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }

            catch (ConflitoException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletarLivro(int id)
        {
            try
            {
                _livroService.DeletarLivro(id);
                return NoContent();
            }

            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }

            catch (ConflitoException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
