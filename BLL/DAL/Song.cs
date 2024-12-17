using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; 

namespace BLL.DAL
{
    public class Song
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Song title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public TimeSpan Duration { get; set; }

        [Required(ErrorMessage = "The Album field is required.")]
        public int AlbumId { get; set; }

        [ValidateNever] 
        public Album Album { get; set; }
    }
}
