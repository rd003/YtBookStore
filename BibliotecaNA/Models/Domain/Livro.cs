using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaNA.Models.Domain
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Isbn { get; set; }
        [Required]
        public int NrPaginas { get; set; }

        [Required]
        public int IdAutor { get; set; }
        [Required]
        public int IdEditora { get; set; }
        [Required]
        public int IdGenero { get; set; }
        public string? ImagePath { get; set; } // Propriedade para armazenar o caminho da imagem

        [NotMapped]
        public string? NomeAutor { get; set; }
        [NotMapped]
        public string? NomeEditora { get; set; }
        [NotMapped]
        public string? NomeGenero { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public List<SelectListItem>? ListaAutor { get; set; }
        [NotMapped]
        public List<SelectListItem>? ListaEditora { get; set; }
        [NotMapped]
        public List<SelectListItem>? ListaGenero { get; set; }

    }
}
