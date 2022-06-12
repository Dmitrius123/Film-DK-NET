using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FilmProject.Controllers
{
    public class FilmsController : Controller
    {
        private readonly FilmDbContext _context;
        public FilmsController(FilmDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var films = await _context.Films.ToListAsync();
            foreach (var item in films)
            {
                item.Actor = _context.Actors.FirstOrDefault(t => t.Id == item.ActorId);
                item.Director = _context.Directors.FirstOrDefault(t => t.Id == item.DirectorId);
                item.Language = _context.Languages.FirstOrDefault(t => t.Id == item.LanguageId);
                item.Genre= _context.Genres.FirstOrDefault(t => t.Id == item.GenreId);
            }
            return View(films);
        }

        public ActionResult Create()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "FirstName");
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "FirstName");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name");
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Title,Description,Budget,ActorId,DirectorId,LanguageId,GenreId,Id")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "FirstName",film.ActorId);
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "FirstName",film.DirectorId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name",film.LanguageId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name",film.GenreId);
            return View(film);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "FirstName", film.ActorId);
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "FirstName", film.DirectorId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", film.LanguageId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            return View(film);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Budget,ActorId,DirectorId,LanguageId,GenreId,Id")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmsExists(film.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "FirstName", film.ActorId);
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "FirstName", film.DirectorId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Name", film.LanguageId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", film.GenreId);
            return View(film);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var films = await _context.Films
                .Include(u => u.Actor)
                .Include(u => u.Director)
                .Include(u=>u.Language)
                .Include(u=>u.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (films == null)
            {
                return NotFound();
            }
            return View(films);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var films = await _context.Films.FindAsync(id);
            _context.Films.Remove(films);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FilmsExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
        }
    }
}
