using BibliotecaNA.Models.Domain;

namespace BibliotecaNA.Repositories.Abstract
{
    public interface IPublisherService
    {
        bool Add(Editora model);
        bool Update(Editora model);
        bool Delete(int id);
        Editora FindById(int id);
        IEnumerable<Editora> GetAll();
    }
}
