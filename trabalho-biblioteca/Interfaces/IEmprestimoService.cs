using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface IEmprestimoService
    {
        public List<Emprestimo> ListarEmprestimos();
        public List<Emprestimo> BuscarEmprestimos(bool somenteAberto);
        public Emprestimo ObterEmprestimoPorId(int id);
        public Emprestimo CriarEmprestimo(EmprestimoRequest request);
        public Emprestimo Editar(int id);
    }
}
