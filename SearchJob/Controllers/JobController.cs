﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Interfaces.Mappers;
using SearchJob.Models;
using SearchJob.Repository;
using System.Diagnostics;

namespace SearchJob.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly JobDbContext _context;
        private readonly IJobRepository _repository;

        public JobController(JobDbContext context, IJobRepository repository)
        {
            _context = context;
            _repository = repository;
            
        }
        [Authorize]
        [HttpGet("GetAllJob")]
        public async Task<IActionResult> GetAll()
        {
            var job = await _repository.GetAsync();

            if (job == null) {

                return NotFound();
            }

            return Ok(job);
        }

        [HttpGet("GetJobById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpDelete("DeleteJobById{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _repository.DeleteAsync(id);
            return Ok();
           
        }

        [HttpPost("CreateJob")]

        public async Task<IActionResult> Create([FromBody] CreateJobRequestDto jobDTO) 
        {

            //var jobModel = new Job {
                
            //    Title = jobDTO.Title,
            //    Description = jobDTO.Description,
            //    CompanyName = jobDTO.CompanyName,
            //    Location = jobDTO.Location,
            //    SalaryFrom  = jobDTO.SalaryFrom,
            //    SalaryTo = jobDTO.SalaryTo,
            //};

            var jobModel = jobDTO.ToJobFromCreateDto();
            var res = await _repository.CreateAsync(jobModel);
            return CreatedAtAction(nameof(GetById), new { id = jobModel.Id }, jobModel.ToJobDto());
        } 
             
       
    }
}

