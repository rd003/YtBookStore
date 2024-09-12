using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;

namespace BibliotecaNA.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUserService service;

        public UsuarioController(IUserService service)
        {
            this.service = service;
        }

        // Exibir formulário para adicionar um novo usuário
        public IActionResult Add()
        {
            return View();
        }

        // Adicionar novo usuário (POST)
        [HttpPost]
        public IActionResult Add(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Usuário adicionado com sucesso!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Ocorreu um erro do lado do servidor.";
            return View(model);
        }

        // Exibir formulário para atualizar um usuário existente
        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }

        // Atualizar usuário (POST)
        [HttpPost]
        public IActionResult Update(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                TempData["msg"] = "Usuário atualizado com sucesso!";
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Ocorreu um erro do lado do servidor.";
            return View(model);
        }

        // Deletar usuário
        public IActionResult Delete(int id)
        {
            var result = service.Delete(id);
            if (result)
            {
                TempData["msg"] = "Usuário deletado com sucesso!";
            }
            else
            {
                TempData["msg"] = "Ocorreu um erro do lado do servidor.";
            }
            return RedirectToAction("GetAll");
        }

        // Listar todos os usuários
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }

//Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            var usuario = service.Login(email, senha);
            if (usuario != null)
            {
                // Cria as Claims de autenticação
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim("UserId", usuario.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "CookieAuth");
                var principal = new ClaimsPrincipal(identity);

                // Registra a autenticação do usuário
                await HttpContext.SignInAsync("CookieAuth", principal);

                return RedirectToAction("Add", "Genero");
            }

            // Retorna uma mensagem de erro se o login falhar
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Faz o logout do usuário
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login", "Usuario");
        }

        [HttpGet]
        public IActionResult AcessoNegado()
        {
            return View();
        }
    }
}