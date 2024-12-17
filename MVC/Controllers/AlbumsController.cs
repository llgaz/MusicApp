using Microsoft.AspNetCore.Mvc;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly AppDbContext _context;

        public AlbumsController()
        {
            _context = new AppDbContext();
        }

        // GET: Albums with Search, Pagination, and Sorting
        public IActionResult Index(string search, string sortOrder, int page = 1)
        {
            int pageSize = 5; // Sayfada gösterilecek eleman sayısı

            // Albümleri çek ve Songs ile ilişkilendir
            var query = _context.Albums.Include(a => a.Songs).AsQueryable();

            // Arama işlemi
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Name.Contains(search));
            }

            // Sıralama işlemi
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(a => a.Name);
                    break;
                case "date":
                    query = query.OrderBy(a => a.ReleaseDate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(a => a.ReleaseDate);
                    break;
                default:
                    query = query.OrderBy(a => a.Name);
                    break;
            }

            // Sayfalama işlemi
            var totalAlbums = query.Count();
            var albums = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // ViewBag ile View'e ekstra bilgileri taşı
            ViewBag.Search = search;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalAlbums / pageSize);
            ViewBag.SortOrder = sortOrder;

            return View(albums);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Album album)
        {
            if (!ModelState.IsValid) // ModelState doğrulamasını kontrol et
            {
                // Hataları loglayalım
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // Bu terminalde hata mesajlarını gösterir
                }
                return View(album);
            }

            try
            {
                _context.Albums.Add(album);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Exception'ı logla ve kullanıcıya hata mesajı göster
                ModelState.AddModelError("", $"An error occurred while saving the album: {ex.Message}");
                return View(album);
            }
        }


    }
}
