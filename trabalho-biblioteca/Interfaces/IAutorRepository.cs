using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Interfaces
{
    public interface IAutorRepository
    {
        public Autor CadastrarAutor(Autor autor);
        public List<Autor> ListarAutores();
        public Autor? ObterAutorPorId(int id);
        public Autor AtualizarAutor(Autor autor);
        public bool AutorPossuiLivro(int id);
        public void DeletarAutor(Autor autor);
    }
}
