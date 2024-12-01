using BibliotecaNA.Models.Domain;

namespace BibliotecaNA.Repositories.Abstract
{
    public interface IBookService
    {
        bool Add(Livro model);
        bool Update(Livro model);
        bool Delete(int id);
        Livro FindById(int id);
        IEnumerable<Livro> GetAll();
    }
}
