using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DAL;
using BLL.Models;

namespace BLL.Services
{
    public class AlbumService
    {
        private readonly AppDbContext _context;

        public AlbumService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AlbumModel> GetAllAlbums()
        {
            return _context.Albums.Select(a => new AlbumModel
            {
                Id = a.Id,
                Name = a.Name,
                ReleaseDate = a.ReleaseDate
            });
        }

        public void AddAlbum(AlbumModel model)
        {
            var album = new Album
            {
                Name = model.Name,
                ReleaseDate = model.ReleaseDate
            };
            _context.Albums.Add(album);
            _context.SaveChanges();
        }
    }
}
