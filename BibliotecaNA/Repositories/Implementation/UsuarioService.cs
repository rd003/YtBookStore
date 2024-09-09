using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;

namespace BibliotecaNA.Repositories.Implementation
{
public class UsuarioService : IUsuarioService
{
    private readonly DatabaseContext _context;

    public UsuarioService(DatabaseContext context)
    {
        _context = context;
    }

    public bool Add(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        return _context.SaveChanges() > 0;
    }

    public bool Update(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
        {
            return false;
        }
        _context.Usuarios.Remove(usuario);
        return _context.SaveChanges() > 0;
    }

    public Usuario FindById(int id)
    {
        return _context.Usuarios.Find(id);
    }

    public IEnumerable<Usuario> GetAll()
    {
        return _context.Usuarios.ToList();
    }
}
}