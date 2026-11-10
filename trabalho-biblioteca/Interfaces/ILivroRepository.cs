using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface ILivroRepository
    {
        public void CadastrarLivro(Livro request);
    }
}
