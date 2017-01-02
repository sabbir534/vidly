using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
        }
        public int? Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
        
        [Display(Name = "Genre"), Required]
        public byte? GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        [Display(Name = "Number in Stock")]

        [Required, Range(1, 20)]
        public byte? NumberInStock { get; set; }
        
        public string ViewTitle { get; set; }
        public string Title => Id != 0 ? "Edit Movie" : "New Movie";
    }
}
