using Microsoft.EntityFrameworkCore;
using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Data
{
    public class ContextoBanco : DbContext
    {
        public ContextoBanco(DbContextOptions<ContextoBanco> options) : base(options) { }

        public DbSet<Autor> Autores { get; set; } = null;
        public DbSet<Emprestimo> Emprestimos { get; set; } = null;
        public DbSet<Livro> Livros { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(autor =>
            {
                autor.HasKey(autor => autor.Id);
                autor.Property(autor => autor.Nome).IsRequired().HasMaxLength(100);
                autor.Property(autor => autor.Nacionalidade).IsRequired().HasMaxLength(50);
                autor.HasMany<Livro>(autor => autor.Livros)
                     .WithOne(livro => livro.Autor)
                     .HasForeignKey(livro => livro.AutorId);
            });

            modelBuilder.Entity<Livro>(livro =>
            {
                livro.HasKey(livro => livro.Id);
                livro.Property(livro => livro.Titulo).IsRequired().HasMaxLength(150);
                livro.HasIndex(livro => livro.Isbn).IsUnique();
                livro.Property(livro => livro.Isbn).IsRequired().HasMaxLength(13);
                livro.Property(livro => livro.AnoPublicacao).IsRequired();
                livro.HasMany<Emprestimo>(livro => livro.Emprestimos)
                     .WithOne(emprestimo => emprestimo.Livro)
                     .HasForeignKey(emprestimo => emprestimo.LivroId);
            });

            modelBuilder.Entity<Emprestimo>(emprestimo =>
            {
                emprestimo.HasKey(emprestimo => emprestimo.Id);
                emprestimo.Property(emprestimo => emprestimo.NomeLeitor).IsRequired().HasMaxLength(100);
                emprestimo.Property(emprestimo => emprestimo.DataEmprestimo).IsRequired();
            });
        }
    }
}
