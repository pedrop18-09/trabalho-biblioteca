using trabalho_biblioteca.Data;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ContextoBanco _contexto;

        public AutorRepository(ContextoBanco contexto)
        {
            _contexto = contexto;
        }

        public Autor AtualizarAutor(Autor autor)
        {
            _contexto.SaveChanges();
            return autor;
        }

        public bool AutorPossuiLivro(int autorId)
        {
            return _contexto.Livros.Any(livro => livro.AutorId == autorId);
        }

        public Autor CadastrarAutor(Autor autor)
        {
            _contexto.Add(autor);
            _contexto.SaveChanges();
            return autor;
        }

        public void DeletarAutor(Autor autor)
        {
            _contexto.Remove(autor);
            _contexto.SaveChanges();
        }

        public List<Autor> ListarAutores()
        {
            return _contexto.Autores.OrderBy(autor => autor.Nome).ToList();
        }

        public Autor? ObterAutorPorId(int id)
        {
            return _contexto.Autores.FirstOrDefault(autor => autor.Id == id);
        }
    }
}
