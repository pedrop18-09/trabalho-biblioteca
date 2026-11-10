namespace trabalho_biblioteca.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public string NomeLeitor { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public virtual Livro? Livro { get; set; }
    }
}
