using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YtBookStore.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Isbn { get; set; }
        [Required]
        public int TotalPages { get; set; }

        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int PubhlisherId { get; set; }
        [Required]
        public int GenreId { get; set; }

        // ** New Lines starts here ** //
        // Related Entities : This feature is added at 01-Feb-2025
        // Reason: I haven't defined foreign keys to this entity, which I am doing now. It won't break the existing code. 
        public Author Author { get; set; } = null!;
        public Publisher Publisher { get; set; } = null!;
        public Genre Genre { get; set; } = null!;

        // ** New Lines ends here ** //

        // Columns not mapped to the DB

        [NotMapped]
        public string? AuthorName { get; set; }
        [NotMapped]
        public string? PublisherName { get; set; }
        [NotMapped]
        public string? GenreName { get; set; }

        [NotMapped]
        public List<SelectListItem>? AuthorList { get; set; }
        [NotMapped]
        public List<SelectListItem>? PublisherList { get; set; }
        [NotMapped]
        public List<SelectListItem>? GenreList { get; set; }

    }
}
