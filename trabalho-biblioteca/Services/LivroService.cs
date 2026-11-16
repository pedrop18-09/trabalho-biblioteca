using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;

        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }

        public Livro CadastrarLivro(CriarLivroRequest request)
        {
            ValidarLivroRequest(request);

            var autor = _autorRepository.ObterAutorPorId(request.AutorId);

            if (autor == null)
            {
                throw new NaoEncontradoException($"Autor com ID {request.AutorId} não encontrado.");
            }

            if (_livroRepository.IsbnExiste(request.Isbn))
            {
                throw new ConflitoException("Já existe um livro com este ISBN.");
            }

            Livro novoLivro = new Livro
            {
                Titulo = request.Titulo.Trim(),
                Isbn = request.Isbn,
                AnoPublicacao = request.AnoPublicacao,
                AutorId = request.AutorId
            };

            _livroRepository.CadastrarLivro(novoLivro);

            return ObterLivroPorId(novoLivro.Id);
        }

        public List<Livro> ListarLivros(int? autorId = null)
        {
            return _livroRepository.ListarLivros(autorId);
        }

        public Livro ObterLivroPorId(int id)
        {
            var livro = _livroRepository.ObterLivroPorId(id);

            if (livro == null)
            {
                throw new NaoEncontradoException($"Livro com ID {id} não encontrado.");
            }

            return livro;
        }

        public Livro AtualizarLivro(int id, CriarLivroRequest request)
        {
            ValidarLivroRequest(request);

            var livroExistente = _livroRepository.ObterLivroPorId(id);

            if (livroExistente == null)
            {
                throw new NaoEncontradoException($"Livro com ID {id} não encontrado.");
            }

            var autor = _autorRepository.ObterAutorPorId(request.AutorId);

            if (autor == null)
            {
                throw new NaoEncontradoException($"Autor com ID {request.AutorId} não encontrado.");
            }

            if (_livroRepository.IsbnExiste(request.Isbn, id))
            {
                throw new ConflitoException("Já existe um livro com este ISBN.");
            }

            livroExistente.Titulo = request.Titulo.Trim();
            livroExistente.Isbn = request.Isbn;
            livroExistente.AnoPublicacao = request.AnoPublicacao;
            livroExistente.AutorId = request.AutorId;

            _livroRepository.AtualizarLivro(livroExistente);

            return ObterLivroPorId(id);
        }

        public void DeletarLivro(int id)
        {
            var livro = _livroRepository.ObterLivroPorId(id);

            if (livro == null)
            {
                throw new NaoEncontradoException($"Livro com ID {id} não encontrado.");
            }

            if (_livroRepository.LivroPossuiEmprestimo(id))
            {
                throw new ConflitoException("Este livro não pode ser excluído, pois possui empréstimos.");
            }

            _livroRepository.DeletarLivro(livro);
        }

        private void ValidarLivroRequest(CriarLivroRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Titulo))
            {
                throw new ValidacaoException("O título do livro é obrigatório.");
            }

            if (request.Titulo.Trim().Length > 150)
            {
                throw new ValidacaoException("O título do livro não pode exceder 150 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(request.Isbn) || request.Isbn.Length != 13 ||
                request.Isbn.Any(digito => digito < '0' || digito > '9'))
            {
                throw new ValidacaoException("O ISBN deve conter exatamente 13 dígitos de 0 a 9, sem espaços ou hífens.");
            }

            if (request.AnoPublicacao < 1000 || request.AnoPublicacao > DateTime.Now.Year)
            {
                throw new ValidacaoException("O ano de publicação deve estar entre 1000 e o ano atual.");
            }
        }
    }
}
