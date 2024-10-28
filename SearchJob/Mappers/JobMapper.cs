using SearchJob.Dtos.Job;
using SearchJob.Models;


namespace SearchJob.Mappers
{
    public static class JobMapper
    {

        public static JobDto ToJobDto(this Job jobModel)
        {
            return new JobDto
            {

                Id = jobModel.Id,
                Title = jobModel.Title,
                Description = jobModel.Description,
                CompanyName = jobModel.CompanyName,
                Location = jobModel.Location,
                SalaryFrom = jobModel.SalaryFrom,
                SalaryTo = jobModel.SalaryTo,
                Url = jobModel.Url,
            };

        }
        public static Job ToJobFromCreateDto(this CreateJobRequestDto jobModel)
        {
            return new Job
            {
                Title = jobModel.Title,
                Description = jobModel.Description,
                CompanyName = jobModel.CompanyName,
                Location = jobModel.Location,
                SalaryFrom = jobModel.SalaryFrom,
                SalaryTo = jobModel.SalaryTo,
                Url = jobModel.Url
            };

        }
        public static Job ToJobFromHH(this HHJob.Item hhJob)
        {
            //return new Job
            //{
            //    Title = hhJob.name,
            //    Description = hhJob.snippet.responsibility,
            //    CompanyName = hhJob.employer.name,
            //    Location = hhJob.area.name,
            //    SalaryFrom = hhJob.salary.from,
            //    SalaryTo = (int)hhJob.salary.to,
            //    Url = hhJob.alternate_url

            //};
            return new Job
            {   
                Id = hhJob.id,
                Title = hhJob?.name ?? "No title",
                Description = hhJob?.snippet?.responsibility ?? "No description available",
                CompanyName = hhJob?.employer?.name ?? "Unknown company",
                Location = hhJob?.area?.name ?? "Unknown location",
                SalaryFrom = hhJob?.salary?.from ?? 0,
                //SalaryTo = hhJob?.salary?.to ?? 0,
                Url = hhJob?.alternate_url ?? "No URL available"
            };


        }

        public static Job ToJobFromDetails(this JobDto jobModel)
        {
            return new Job
            {
                Id = jobModel.Id,
                Title = jobModel.Title,
                Description = jobModel.Description,
                CompanyName = jobModel.CompanyName,
                Location = jobModel.Location,
                SalaryFrom = jobModel.SalaryFrom,
                SalaryTo = jobModel.SalaryTo,
                Url = jobModel.Url
            };

        }
    }
}
