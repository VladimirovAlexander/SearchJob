using System.ComponentModel.DataAnnotations.Schema;

namespace SearchJob.Models
{
    [Table("Favorites")]
    public class Favorite
    {
        public string AppUserId { get; set; }
        
        public int JobId { get; set; } 

        public AppUser AppUser { get; set; }

        public Job Job { get; set; }    

    }
}
