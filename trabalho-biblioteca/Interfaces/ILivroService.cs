using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface ILivroService
    {
        public Livro CadastrarLivro(CriarLivroRequest request);
        public List<Livro> ListarLivros(int? autorId = null);
        public Livro ObterLivroPorId(int id);
        public Livro AtualizarLivro(int id, CriarLivroRequest request);
        public void DeletarLivro(int id);
    }
}
