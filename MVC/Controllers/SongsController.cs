using Microsoft.AspNetCore.Mvc;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class SongsController : Controller
    {
        private readonly AppDbContext _context;

        public SongsController()
        {
            _context = new AppDbContext();
        }

        // GET: Songs
        public IActionResult Index()
        {
            var songs = _context.Songs.Include(s => s.Album).ToList();
            return View(songs);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewBag.Albums = _context.Albums.ToList();

            if (ViewBag.Albums == null || !_context.Albums.Any())
            {
                Console.WriteLine("No albums found!");
            }

            return View();
        }

        // POST: Songs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Song song)
        {
            Console.WriteLine($"AlbumId: {song.AlbumId}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid");
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.Albums = _context.Albums.ToList();
                return View(song);
            }

            try
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
                Console.WriteLine("Song saved successfully!");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred while saving the song.");
                ViewBag.Albums = _context.Albums.ToList();
                return View(song);
            }
        }

    }
}
