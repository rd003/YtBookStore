using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace BibliotecaNA.Controllers
{
    public class LivroController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LivroController(IBookService bookService, IGenreService genreService, IPublisherService publisherService, IAuthorService authorService, IWebHostEnvironment webHostEnvironment)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Add()
        {
            var model = new Livro();
            model.ListaAutor = authorService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
            model.ListaEditora = publisherService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
            model.ListaGenero = genreService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Livro model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // Gerar um nome de arquivo único
                string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.ImageFile.FileName);

                // Definir o caminho para salvar o arquivo
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", fileName);

                using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                {
                    // Salvar o arquivo no caminho especificado
                    model.ImageFile.CopyTo(fileStream);
                }

                // Salvar o caminho da imagem no modelo
                model.ImagePath = "/upload/" + fileName;
            }

            if (!ModelState.IsValid)
            {
                model.ListaAutor = authorService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
                model.ListaEditora = publisherService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
                model.ListaGenero = genreService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString() }).ToList();
                return View(model);
            }

            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Livro adicionado com sucesso";
                return RedirectToAction(nameof(Add));
            }

            TempData["msg"] = "Erro ao adicionar o livro, certifique-se de preencher todos os campos!";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var model = bookService.FindById(id);
            model.ListaAutor = authorService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdAutor }).ToList();
            model.ListaEditora = publisherService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdEditora }).ToList();
            model.ListaGenero = genreService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdGenero }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Livro model)
        {
            model.ListaAutor = authorService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdAutor }).ToList();
            model.ListaEditora = publisherService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdEditora }).ToList();
            model.ListaGenero = genreService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(), Selected = a.Id == model.IdGenero }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Lógica de upload de imagem
            if (model.ImageFile != null)
            {
                // Gera um nome único para a imagem
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;

                // Caminho completo para armazenar a imagem
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "upload");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Certifique-se de que a pasta "uploads" existe
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Salva o arquivo
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }

                // Armazena o caminho da imagem no banco de dados
                model.ImagePath = "/upload/" + uniqueFileName;
            }

            var result = bookService.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Erro ao adicionar o livro, certifique-se de preencher todos os campos!";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = bookService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = bookService.GetAll();
            return View(data);
        }
    }
}
