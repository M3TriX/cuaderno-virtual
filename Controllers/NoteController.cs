using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cuaderno_virtual.Models;
using Cuaderno_virtual.Data;
using Cuaderno_virtual.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuaderno_virtual.Controllers;
//controlador de las vistas que se harán
public class NoteController : Controller
{
    //_context es una variable privada del controlador, y este contendrá el contexto de la base de datos
    private readonly CuadernowebContext _context;
    //Con este constructor tendré acceso a la base de datos
    public NoteController(CuadernowebContext context)
    {
        _context = context;
    }

    public IActionResult Index(string orden)
    {
        return View(_context.Notes.ToList());
    }
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NoteViewModel model)
    {
        if(ModelState.IsValid)
        {
            var note = new Note()
            {
                Title = model.title,
                Body = model.body,
                Date = new DateOnly(model.date.Year,model.date.Month,model.date.Day),
                Active = true
            };
            
            _context.Add(note);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
