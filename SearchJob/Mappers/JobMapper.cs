﻿using SearchJob.Dtos.Job;
using SearchJob.Dtos.Job.HHJob;
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
        public static Job ToJobFromHH(this Item hhJob)
        {
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
