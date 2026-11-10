using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livrorepository;
        public LivroService(ILivroRepository livroRepository)
        {
            _livrorepository = livroRepository;
        }

        public Livro CadastrarLivro(CriarLivroRequest request)
        {
            try
            {
                
                Livro novoLivro = new Livro()
                {
                    Titulo = request.Titulo,
                    Isbn = request.Isbn,
                    AnoPublicacao = request.AnoPublicacao,
                    AutorId = request.AutorId,
                };

                _livrorepository.CadastrarLivro(novoLivro);

                return novoLivro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
