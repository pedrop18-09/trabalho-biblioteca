using trabalho_biblioteca.Models;

namespace trabalho_biblioteca.Dto
{
    public class CriarLivroRequest
    {
        public string Titulo { get; set; }
        public string Isbn { get; set; }
        public int AnoPublicacao { get; set; }
        public int AutorId { get; set; }
    }
}
