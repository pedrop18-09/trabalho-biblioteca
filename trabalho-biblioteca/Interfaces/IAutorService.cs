using trabalho_biblioteca.Dto;

namespace trabalho_biblioteca.Interfaces
{
    public interface IAutorService
    {
        public AutorResponse CadastrarAutor(AutorRequest request);
        public List<AutorResponse> ListarAutores();
        public AutorResponse ObterAutorPorId(int id);
        public AutorResponse AtualizarAutor(int id, AutorRequest request);
        public void DeletarAutor(int id);
    }
}
