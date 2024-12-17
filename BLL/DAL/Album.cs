using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Album
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Album name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Release date is required")]
        public DateTime ReleaseDate { get; set; }

        
        public ICollection<Song>? Songs { get; set; } = new List<Song>();
    }
}


