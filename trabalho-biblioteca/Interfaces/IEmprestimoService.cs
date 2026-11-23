using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface IEmprestimoService
    {
        public List<Emprestimo> ListarEmprestimos(bool somenteAbertos = false);
        public Emprestimo ObterEmprestimoPorId(int id);
        public Emprestimo CriarEmprestimo(EmprestimoRequest request);
        public Emprestimo Editar(int id);
    }
}
