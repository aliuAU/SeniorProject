using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmFolio.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string MovieName { get; set; }

        public string MovieDescription { get; set; }

        [Required]
        public string Director { get; set;}

        [ValidateNever]
        public int MovieGenreId { get; set; }
        [ForeignKey("MovieGenreId")]

        [ValidateNever]
        public MovieGenre Genre { get; set; }

        [ValidateNever]
        public string ImageURL { get; set; }

        [Range(1900,2030)]
        public int ReleaseYear {  get; set; }

        public List<Comment> Comments { get; set; }
        public double AverageRating { get; set; }
    }
}
