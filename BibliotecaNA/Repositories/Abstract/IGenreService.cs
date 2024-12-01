using BibliotecaNA.Models.Domain;

namespace BibliotecaNA.Repositories.Abstract
{
    public interface IGenreService
    {
        bool Add(Genero model);
        bool Update(Genero model);
        bool Delete(int id);
        Genero FindById(int id);
        IEnumerable<Genero> GetAll();
    }
}
