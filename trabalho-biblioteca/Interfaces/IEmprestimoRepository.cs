using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface IEmprestimoRepository
    {
        public List<Emprestimo> ListarEmprestimos(bool somenteAbertos = false);
        public Emprestimo? ObterEmprestimoPorId(int id);
        public bool VerificarEmprestimoAberto(int livroId);
        public Emprestimo CriarEmprestimo(Emprestimo emprestimo);
        public Emprestimo Editar(Emprestimo emprestimo);
    }
}
