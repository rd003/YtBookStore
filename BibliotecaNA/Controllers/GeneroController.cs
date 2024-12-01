using Microsoft.AspNetCore.Mvc;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaNA.Controllers
{
    public class GeneroController : Controller
    {
        private readonly IGenreService service;
        public GeneroController(IGenreService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genero model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Genero adicionado com sucesso!";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Erro! Tente novamente mais tarde";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Genero model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("GetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {

            var data = service.GetAll();
            return View(data);
        }

    }
}
