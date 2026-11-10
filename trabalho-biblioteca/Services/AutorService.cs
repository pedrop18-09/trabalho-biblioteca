using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public AutorResponse AtualizarAutor(int id, AutorRequest request)
        {
            ValidarAutorRequest(request);
            var autorExistente = _autorRepository.ObterAutorPorId(id);
            if (autorExistente == null)
            {
                throw new NaoEncontradoException($"Autor com ID {id} não encontrado.");
            }

            autorExistente.Nome = request.Nome.Trim();
            autorExistente.Nacionalidade = request.Nacionalidade.Trim();

            _autorRepository.AtualizarAutor(autorExistente);

            return FormatarResposta(autorExistente);
        }

        public AutorResponse CadastrarAutor(AutorRequest request)
        {
            ValidarAutorRequest(request);

            Autor novoAutor = new Autor
            {
                Nome = request.Nome.Trim(),
                Nacionalidade = request.Nacionalidade.Trim()
            };

            _autorRepository.CadastrarAutor(novoAutor);

            return FormatarResposta(novoAutor);
        }

        public void DeletarAutor(int id)
        {
            var autor = _autorRepository.ObterAutorPorId(id);

            if (autor == null)
            {
                throw new NaoEncontradoException($"Autor com ID {id} não encontrado.");
            }

            bool possuiLivro = _autorRepository.AutorPossuiLivro(id);

            if (possuiLivro)
            {
                throw new ConflitoException("Este autor não pode ser excluído, pois possui livros.");
            }

            _autorRepository.DeletarAutor(autor);
        }

        public List<AutorResponse> ListarAutores()
        {
            var autores = _autorRepository.ListarAutores();

            return autores.Select(FormatarResposta).ToList();
        }

        public AutorResponse ObterAutorPorId(int id)
        {
            var autor = _autorRepository.ObterAutorPorId(id);
            
            if (autor == null)
            {
                throw new NaoEncontradoException($"Autor com ID {id} não encontrado.");
            }

            return FormatarResposta(autor);
        }
        
        private void ValidarAutorRequest(AutorRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nome))
            {
                throw new ValidacaoException("O nome do autor é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Nacionalidade))
            {
                throw new ValidacaoException("A nacionalidade do autor é obrigatória.");
            }

            string nomeLimpo = request.Nome.Trim();
            string nacionalidadeLimpa = request.Nacionalidade.Trim();

            if (nomeLimpo.Length > 100)
            {
                throw new ValidacaoException("O nome do autor não pode exceder 100 caracteres.");
            }

            if (nacionalidadeLimpa.Length > 50)
            {
                throw new ValidacaoException("A nacionalidade do autor não pode exceder 50 caracteres.");
            }
        }

        private AutorResponse FormatarResposta(Autor autor)
        {
            return new AutorResponse
            {
                Id = autor.Id,
                Nome = autor.Nome,
                Nacionalidade = autor.Nacionalidade
            };
        }
    }
}
