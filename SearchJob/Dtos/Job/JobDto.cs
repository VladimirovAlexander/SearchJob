﻿using System.ComponentModel.DataAnnotations;

namespace SearchJob.Dtos.Job
{
    public class JobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string CompanyName { get; set; } 

        public string Location { get; set; } 

        public int? SalaryFrom { get; set; }

        public int? SalaryTo { get; set; }

        public string Url { get; set; } 

        public DateTime PostedDate { get; set; }
    }
}
