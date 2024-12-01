using BibliotecaNA.Models.Domain;

namespace BibliotecaNA.Repositories.Abstract
{
    public interface IAuthorService
    {
        bool Add(Autor model);
        bool Update(Autor model);
        bool Delete(int id);
        Autor FindById(int id);
        IEnumerable<Autor> GetAll();
    }
}
