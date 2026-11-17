using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface IEmprestimoRepository
    {
        public List<Emprestimo> ListarEmprestimos();
        public List<Emprestimo> BuscarEmprestimos(bool somenteAberto);
        public Emprestimo ObterEmprestimoPorId(int id);
        public bool VerificarEmprestimoAberto (int id);
        public Emprestimo CriarEmprestimo(EmprestimoRequest request);
        public Emprestimo Editar(Emprestimo emprestimo);
    }
}
