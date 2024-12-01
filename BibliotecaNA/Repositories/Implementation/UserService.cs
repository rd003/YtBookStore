using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;
using System.Linq;

namespace BibliotecaNA.Repositories.Implementation
{
public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public bool Add(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        return _context.SaveChanges() > 0;
    }

    public bool Update(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var usuario = _context.Usuario.Find(id);
        if (usuario == null)
        {
            return false;
        }
        _context.Usuario.Remove(usuario);
        return _context.SaveChanges() > 0;
    }

    public Usuario FindById(int id)
    {
        return _context.Usuario.Find(id);
    }

    public IEnumerable<Usuario> GetAll()
    {
        return _context.Usuario.ToList();
    }

    public Usuario Login(string email, string senha)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
    }    
}
}