using Microsoft.EntityFrameworkCore;
using trabalho_biblioteca.Data;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ContextoBanco _contexto;

        public LivroRepository(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public Livro CadastrarLivro(Livro livro)
        {
            _contexto.Livros.Add(livro);
            _contexto.SaveChanges();
            return livro;
        }

        public List<Livro> ListarLivros(int? autorId = null)
        {
            IQueryable<Livro> livros = _contexto.Livros.Include(livro => livro.Autor);

            if (autorId.HasValue)
            {
                livros = livros.Where(livro => livro.AutorId == autorId.Value);
            }

            return livros.OrderBy(livro => livro.Titulo).ToList();
        }

        public Livro? ObterLivroPorId(int id)
        {
            return _contexto
                .Livros.Include(livro => livro.Autor)
                .FirstOrDefault(livro => livro.Id == id);
        }

        public Livro AtualizarLivro(Livro livro)
        {
            _contexto.SaveChanges();
            return livro;
        }

        public bool IsbnExiste(string isbn, int? livroId = null)
        {
            return _contexto.Livros.Any(livro =>
                livro.Isbn == isbn && (livroId == null || livro.Id != livroId)
            );
        }

        public bool LivroPossuiEmprestimo(int id)
        {
            return _contexto.Emprestimos.Any(emprestimo => emprestimo.LivroId == id);
        }

        public void DeletarLivro(Livro livro)
        {
            _contexto.Livros.Remove(livro);
            _contexto.SaveChanges();
        }
    }
}
