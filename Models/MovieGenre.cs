using System.ComponentModel.DataAnnotations;

namespace FilmFolio.Models
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Genre is required.")] //film türü null olamaz
        [MaxLength(25)]
        public string Genre { get; set; }   //primary key değil aynı türden birçok olabilir

    }
}
