namespace SearchJob.Dtos.Job.HHJob
{
    public class Employer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string alternate_url { get; set; }
        public object logo_urls { get; set; }
        public string vacancies_url { get; set; }
        public bool? accredited_it_employer { get; set; }
        public bool? trusted { get; set; }
    }
}
