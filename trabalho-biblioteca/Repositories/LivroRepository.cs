using trabalho_biblioteca.Data;
using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly ContextoBanco _db;

        public LivroRepository(ContextoBanco db)
        {
            _db = db;
        }

        public void CadastrarLivro(Livro request)
        {
            _db.Livros.Add(request);
            _db.SaveChanges(); 

        }
    }
}
