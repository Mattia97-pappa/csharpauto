using Inventary.Data;
using Inventary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var admin = _context.Admin.SingleOrDefault(a => a.username == username);

        if (admin == null || !VerifyPassword(password, admin.password))
        {
            ModelState.AddModelError("", "Credenziali non valide");
            return View();
        }

        HttpContext.Session.SetInt32("AdminId", admin.id);

        return RedirectToAction("Dashboard");
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        return inputPassword == storedPassword;
    }

    public IActionResult Dashboard(string searchCode)
    {
        if (!HttpContext.Session.Keys.Contains("AdminId"))
            return RedirectToAction("Login");

        var prodotti = _context.Componenti.AsQueryable();

        if (!string.IsNullOrEmpty(searchCode))
        {
            prodotti = prodotti.Where(p => p.codice.Contains(searchCode));
            TempData["Messaggio"] = $"Risultati per '{searchCode}'";
        }

        return View(prodotti.ToList());
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
