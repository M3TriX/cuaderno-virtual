using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Cuaderno_virtual.Models;
using Cuaderno_virtual.Data;
using Cuaderno_virtual.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Index(int orden,string searchString)
    {
        var notes1 = from s in _context.Notes select s;
        if (!String.IsNullOrEmpty(searchString))
        {
            notes1 = notes1.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
        }
        switch(orden){
            case 1:
                notes1 = notes1.OrderBy(s => s.Title);
                break;
            case 2:
                notes1 = notes1.OrderBy(s => s.Body);
                break;
            case 3:
                notes1 = notes1.OrderBy(s => s.Date);
                break;
            default:
                notes1 = notes1.OrderBy(s => s.Title);
                break;
        }
        return View(notes1.ToList());
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
                Title = model.Title,
                Body = model.Body,
                Date = new DateOnly(model.Date.Year,model.Date.Month,model.Date.Day),
                Active = true
            };
            
            _context.Add(note);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    public IActionResult Details(int Id){
        var note = _context.Notes.Find(Id);
        if (note == null)
        {
            return NotFound();
        }
        return View(note);
    }

    public IActionResult Edit(int Id){
        var note = _context.Notes.Find(Id);
        if (note == null)
        {
            return NotFound();
        }
        var viewnote = new NoteViewModel()
            {
                Id = note.Id,
                Title = note.Title,
                Body = note.Body,
                Date = new DateTime(note.Date.Year,note.Date.Month,note.Date.Day)
            };
        return View(viewnote);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(NoteViewModel model)
    {
        if(ModelState.IsValid)
        {
            var note = new Note()
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Date = new DateOnly(model.Date.Year,model.Date.Month,model.Date.Day),
                Active = true
            };
            
            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }
}
