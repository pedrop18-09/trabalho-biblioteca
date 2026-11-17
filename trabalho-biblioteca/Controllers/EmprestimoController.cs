using Microsoft.AspNetCore.Mvc;
using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoService _emprestimoService;

        public EmprestimoController(IEmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpGet]
        public IActionResult ListarEmprestimos()
        {
            return Ok(_emprestimoService.ListarEmprestimos());
        }

        [HttpGet]
        public IActionResult BuscarEmprestimos([FromQuery] bool buscarEmprestimos)
        {
            return Ok(_emprestimoService.BuscarEmprestimos(buscarEmprestimos));
        }

        [HttpGet("{id:int}")]
        public IActionResult ObterEmprestimoPorId(int id)
        {
            try
            {
                return Ok(_emprestimoService.ObterEmprestimoPorId(id));
            }
            catch(NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CriarEmprestimo(EmprestimoRequest request)
        {
            try
            {
                var emprestimo = _emprestimoService.CriarEmprestimo(request);

                return CreatedAtAction(nameof(ObterEmprestimoPorId), new { id = emprestimo.Id }, emprestimo);
            }
            catch(ValidacaoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch(ConflitoException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id:int}/devolucao")]
        public IActionResult DevolverLivro(int id)
        {
            try
            {
                return Ok(_emprestimoService.Editar(id));
            }
            catch (NaoEncontradoException ex)
            {
                return NotFound(ex.Message);
            }
            catch(ConflitoException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
