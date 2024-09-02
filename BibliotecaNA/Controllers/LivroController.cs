using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;

namespace BibliotecaNA.Controllers
{
    public class LivroController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IPublisherService publisherService;
        public LivroController(IBookService bookService, IGenreService genreService, IPublisherService publisherService,IAuthorService authorService)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.publisherService = publisherService;
            this.authorService = authorService;
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
            model.ListaAutor = authorService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(),Selected=a.Id==model.IdAutor}).ToList();
            model.ListaEditora = publisherService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(),Selected=a.Id==model.IdEditora }).ToList();
            model.ListaGenero = genreService.GetAll().Select(a => new SelectListItem { Text = a.Nome, Value = a.Id.ToString(),Selected=a.Id==model.IdGenero }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error has occured on server side";
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
            var result = bookService.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occured on server side";
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
