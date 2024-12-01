using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;

namespace BibliotecaNA.Repositories.Implementation
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext context;
        public BookService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Livro model)
        {
            try
            {
                context.Livro.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Livro.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Livro FindById(int id)
        {
            return context.Livro.Find(id);
        }

        public IEnumerable<Livro> GetAll()
        {
            var data = (from book in context.Livro
                        join author in context.Autor
                        on book.IdAutor equals author.Id
                        join publisher in context.Editora on book.IdEditora equals publisher.Id
                        join genre in context.Genero on book.IdGenero equals genre.Id
                        select new Livro
                        {
                            Id = book.Id,
                            IdAutor = book.IdAutor,
                            IdGenero = book.IdGenero,
                            Isbn = book.Isbn,
                            IdEditora = book.IdEditora,
                            Titulo = book.Titulo,
                            NrPaginas = book.NrPaginas,
                            NomeGenero = genre.Nome,
                            NomeAutor = author.Nome,
                            NomeEditora = publisher.Nome,
                            ImagePath = book.ImagePath
                        }
                        ).ToList();
            return data;
        }

        public bool Update(Livro model)
        {
            try
            {
                var existingLivro = context.Livro.Find(model.Id);
                if (existingLivro == null)
                {
                    return false; // Livro não encontrado
                }

                // Atualize as propriedades do livro existente
                existingLivro.Titulo = model.Titulo;
                existingLivro.Isbn = model.Isbn;
                existingLivro.NrPaginas = model.NrPaginas;
                existingLivro.IdAutor = model.IdAutor;
                existingLivro.IdEditora = model.IdEditora;
                existingLivro.IdGenero = model.IdGenero;
                existingLivro.ImagePath = model.ImagePath;

                // Salve as mudanças
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log da exceção para depuração
                Console.WriteLine($"Error updating livro: {ex.Message}");
                return false;
            }
        }

    }
}
