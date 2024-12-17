using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.DAL;
using BLL.Models;

namespace BLL.Services
{
    public class SongService
    {
        private readonly AppDbContext _context;

        public SongService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SongModel> GetAllSongs()
        {
            return _context.Songs.Select(s => new SongModel
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                AlbumId = s.AlbumId
            });
        }

        public void AddSong(SongModel model)
        {
            var song = new Song
            {
                Title = model.Title,
                Duration = model.Duration,
                AlbumId = model.AlbumId
            };

            _context.Songs.Add(song);
            _context.SaveChanges();
        }
    }
}

