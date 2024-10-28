using SearchJob.Models;

namespace SearchJob.Helper
{
    public class PaginatedJobViewModel
    {
        public List<Job> Jobs { get; set; } = new List<Job>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public int TotalJobs { get; set; }
    }
}
