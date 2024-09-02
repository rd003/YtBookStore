using System.ComponentModel.DataAnnotations;

namespace BibliotecaNA.Models.Domain
{
    public class Editora
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
