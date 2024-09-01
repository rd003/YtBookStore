using System.ComponentModel.DataAnnotations;

namespace BibliotecaNA.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
