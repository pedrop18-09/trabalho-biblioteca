using Microsoft.AspNetCore.Mvc;
using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

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
                Livro livro = _livroService.CadastrarLivro(request);
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
