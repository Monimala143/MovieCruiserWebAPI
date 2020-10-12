using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class MovieList
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string MovieName { get; set; }

        public int TicketPrice { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public string Genere { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
