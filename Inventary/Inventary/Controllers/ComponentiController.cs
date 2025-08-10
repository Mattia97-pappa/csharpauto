using Microsoft.AspNetCore.Mvc;
using Inventary.Data;
using Inventary.Models;
using System.Linq;

namespace Inventary.Controllers
{
    public class ComponentiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string categoria)
        {
            var componenti = _context.Componenti.AsQueryable();

            if (!string.IsNullOrEmpty(categoria))
            {
                componenti = componenti.Where(c =>
                    c.categoria != null &&
                    c.categoria.ToLower() == categoria.ToLower()
                );
            }

            return View(componenti.ToList());
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var componente = _context.Componenti.FirstOrDefault(c => c.id == id);
            if (componente == null)
                return NotFound();

            return View(componente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var componente = _context.Componenti.FirstOrDefault(c => c.id == id);
            if (componente == null)
                return NotFound();

            _context.Componenti.Remove(componente);
            _context.SaveChanges();

            TempData["Messaggio"] = "Articolo eliminato con successo.";

            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var componente = _context.Componenti.FirstOrDefault(c => c.id == id);
            if (componente == null)
                return NotFound();

            return View(componente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Componente componente)
        {
            if (!ModelState.IsValid)
                return View(componente);

            var componenteInDb = _context.Componenti.FirstOrDefault(c => c.id == componente.id);
            if (componenteInDb == null)
                return NotFound();

            componenteInDb.nome = componente.nome;
            componenteInDb.descrizione = componente.descrizione;
            componenteInDb.immagine = componente.immagine;
            componenteInDb.giacenza = componente.giacenza;
            componenteInDb.prezzo = componente.prezzo;
            componenteInDb.categoria = componente.categoria;
            componenteInDb.codice = componente.codice;

            _context.SaveChanges();

            TempData["Messaggio"] = "Articolo modificato con successo.";

            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Componente componente)
        {
            if (!ModelState.IsValid)
                return View(componente);

            _context.Componenti.Add(componente);
            _context.SaveChanges();

            TempData["Messaggio"] = "Articolo creato con successo.";

            return RedirectToAction("Dashboard", "Admin");
        }
    }
}
