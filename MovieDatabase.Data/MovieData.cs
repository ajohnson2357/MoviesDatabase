using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Data
{
    public class MovieData
    {
        [Key]
        public int MovieId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage="Title must be a minimum of two characters.")]
        [MaxLength(50, ErrorMessage="The title is too long.")]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Display(Name ="Run Time, in minutes")]
        public float RunTime { get; set; }
        [MaxLength(100, ErrorMessage ="Description is too long.")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Display(Name ="Has this been watched?")]
        public bool HaveWatched { get; set; }
        [Display(Name ="Favorite?")]
        public bool IsStarred { get; set; }
        [Display(Name ="Rating, 0 through 5")]
        [Range(0,5,ErrorMessage = "please choose a rating between 0 and 5")]
        public int MovieQuality { get; set; }

        [Display(Name ="Rating, G, PG, etc")]
        public string MovieRating { get; set; }
    }
}
