using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchJob.Models
{
    [Table("Jobs")]
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CompanyName { get; set; }= string.Empty;
        
        public string Location { get; set; } = string.Empty;
        
        public decimal? SalaryFrom { get; set; } 
        
        public decimal? SalaryTo { get; set; }
        public string? Url { get; set; } = string.Empty;

        public DateTime PostedDate { get; set; }

        public List<Favorite> Favorites = new List<Favorite>();

    }
}
