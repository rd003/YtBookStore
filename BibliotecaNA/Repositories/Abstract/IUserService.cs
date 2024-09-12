using BibliotecaNA.Models.Domain;
using System.Collections.Generic;

namespace BibliotecaNA.Repositories.Abstract
{
public interface IUserService
{
    bool Add(Usuario usuario);
    bool Update(Usuario usuario);
    bool Delete(int id);
    Usuario FindById(int id);
    IEnumerable<Usuario> GetAll();
    Usuario Login(string email, string senha);
}
}