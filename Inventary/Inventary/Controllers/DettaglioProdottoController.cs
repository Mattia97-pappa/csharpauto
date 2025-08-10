using Microsoft.AspNetCore.Mvc;
using Inventary.Data;
using Inventary.Models;
using System.Linq;

namespace Inventary.Controllers
{
    public class DettaglioProdottoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DettaglioProdottoController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index(int id)
        {
            var componente = _context.Componenti.FirstOrDefault(c => c.id == id);
            if (componente == null)
            {
                return NotFound();
            }
            return View(componente);
        }
    }
}
