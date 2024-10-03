using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchJob.Data;
using SearchJob.Dtos.Job;
using SearchJob.Interfaces;
using SearchJob.Models;
using SearchJob.Repository;

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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var job = await _repository.GetAsync();

            if (job == null) {

                return NotFound();
            }

            return Ok(job);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _repository.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _repository.DeleteAsync(id);
            return Ok();
           
        }
             
       
    }
}

