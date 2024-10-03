using SearchJob.Dtos.Job;
using SearchJob.Models;

namespace SearchJob.Mappers
{
    public static class JobMapper
    {

        public static Job ToJobFromHH(this HHJob hhJob)
        {
            return new Job
            {
                Title = hhJob.,
                Description = hhJob.snippet.responsibility,
                CompanyName = hhJob.employer.name ,
                Location = hhJob.area.name,
                SalaryFrom = hhJob.salary.from ,
                SalaryTo = (int)hhJob.salary.to,
                Url = hhJob.alternate_url
            };


        }
    }
}
