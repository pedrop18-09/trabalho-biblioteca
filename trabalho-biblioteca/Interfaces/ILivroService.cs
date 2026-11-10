using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface ILivroService
    {
        public Livro CadastrarLivro(CriarLivroRequest request);
    }
}
