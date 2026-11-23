using Microsoft.EntityFrameworkCore;
using trabalho_biblioteca.Data;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly ContextoBanco _contexto;

        public EmprestimoRepository(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public List<Emprestimo> ListarEmprestimos(bool somenteAbertos = false)
        {
            IQueryable<Emprestimo> emprestimos = _contexto.Emprestimos
                .Include(emprestimo => emprestimo.Livro)
                .ThenInclude(livro => livro!.Autor);

            if (somenteAbertos)
            {
                emprestimos = emprestimos.Where(emprestimo => emprestimo.DataDevolucao == null);
            }

            return emprestimos.OrderByDescending(emprestimo => emprestimo.DataEmprestimo).ToList();
        }

        public Emprestimo? ObterEmprestimoPorId(int id)
        {
            return _contexto.Emprestimos
                .Include(emprestimo => emprestimo.Livro)
                .ThenInclude(livro => livro!.Autor)
                .FirstOrDefault(emprestimo => emprestimo.Id == id);
        }

        public bool VerificarEmprestimoAberto(int livroId)
        {
            return _contexto.Emprestimos.Any(emprestimo =>
                emprestimo.LivroId == livroId && emprestimo.DataDevolucao == null);
        }

        public Emprestimo CriarEmprestimo(Emprestimo emprestimo)
        {
            _contexto.Emprestimos.Add(emprestimo);
            _contexto.SaveChanges();
            return emprestimo;
        }

        public Emprestimo Editar(Emprestimo emprestimo)
        {
            _contexto.SaveChanges();
            return emprestimo;
        }
    }
}
