using System.Text.Json.Serialization;

namespace trabalho_biblioteca.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Nacionalidade { get; set; }
        [JsonIgnore]
        public virtual List<Livro> Livros { get; set; }
    }
}
