using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface ILivroRepository
    {
        public Livro CadastrarLivro(Livro livro);
        public List<Livro> ListarLivros(int? autorId = null);
        public Livro? ObterLivroPorId(int id);
        public Livro AtualizarLivro(Livro livro);
        public bool IsbnExiste(string isbn, int? livroId = null);
        public bool LivroPossuiEmprestimo(int id);
        public void DeletarLivro(Livro livro);
    }
}
