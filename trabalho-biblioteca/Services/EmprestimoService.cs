using trabalho_biblioteca.Dto;
using trabalho_biblioteca.Exceptions;
using trabalho_biblioteca.Interfaces;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public List<Emprestimo> ListarEmprestimos(bool somenteAbertos = false)
        {
            return _emprestimoRepository.ListarEmprestimos(somenteAbertos);
        }

        public Emprestimo ObterEmprestimoPorId(int id)
        {
            var emprestimo = _emprestimoRepository.ObterEmprestimoPorId(id);

            if (emprestimo == null)
            {
                throw new NaoEncontradoException($"Empréstimo com ID {id} não encontrado.");
            }

            return emprestimo;
        }

        public Emprestimo CriarEmprestimo(EmprestimoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NomeLeitor))
            {
                throw new ValidacaoException("O nome do leitor é obrigatório.");
            }

            string nomeLeitor = request.NomeLeitor.Trim();

            if (nomeLeitor.Length > 100)
            {
                throw new ValidacaoException("O nome do leitor não pode exceder 100 caracteres.");
            }

            var livro = _livroRepository.ObterLivroPorId(request.LivroId);

            if (livro == null)
            {
                throw new NaoEncontradoException($"Livro com ID {request.LivroId} não encontrado.");
            }

            if (_emprestimoRepository.VerificarEmprestimoAberto(request.LivroId))
            {
                throw new ConflitoException("Este livro já está emprestado.");
            }

            Emprestimo novoEmprestimo = new Emprestimo
            {
                LivroId = request.LivroId,
                NomeLeitor = nomeLeitor,
                DataEmprestimo = DateTime.Now,
                DataDevolucao = null
            };

            _emprestimoRepository.CriarEmprestimo(novoEmprestimo);

            return ObterEmprestimoPorId(novoEmprestimo.Id);
        }

        public Emprestimo Editar(int id)
        {
            var emprestimo = _emprestimoRepository.ObterEmprestimoPorId(id);

            if (emprestimo == null)
            {
                throw new NaoEncontradoException($"Empréstimo com ID {id} não encontrado.");
            }

            if (emprestimo.DataDevolucao.HasValue)
            {
                throw new ConflitoException("Este empréstimo já foi devolvido.");
            }

            emprestimo.DataDevolucao = DateTime.Now;

            _emprestimoRepository.Editar(emprestimo);

            return ObterEmprestimoPorId(id);
        }
    }
}
