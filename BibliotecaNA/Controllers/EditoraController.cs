using Microsoft.AspNetCore.Mvc;
using BibliotecaNA.Models.Domain;
using BibliotecaNA.Repositories.Abstract;

namespace BibliotecaNA.Controllers
{
    public class EditoraController : Controller
    {
        private readonly IPublisherService service;
        public EditoraController(IPublisherService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Editora model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Editora adicionada com sucesso!";
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
        public IActionResult Update(Editora model)
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
            TempData["msg"] = "Erro! Tente novamente mais tarde";
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
