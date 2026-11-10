using System.Text.Json.Serialization;

namespace trabalho_biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int AnoPublicacao { get; set; }
        public int AutorId { get; set; }
        public virtual Autor? Autor { get; set; }
        [JsonIgnore]
        public virtual List<Emprestimo> Emprestimos { get; set; }

    }
}
