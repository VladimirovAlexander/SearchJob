using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchJob.Data;
using SearchJob.Dtos.Job;
using SearchJob.Helper;
using SearchJob.Interfaces;
using SearchJob.Mappers;
using SearchJob.Models;


namespace SearchJob.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobController : Controller
    {
        private readonly JobDbContext _context;
        private readonly IJobRepository _repository;
        private readonly IHHService _service;
        public JobController(JobDbContext context, IJobRepository repository, IHHService service)
        {
            _context = context;
            _repository = repository;
            _service = service;
        }

        [HttpGet("GetAllJob")]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 6)
        {

            var jobFromDb = await _repository.GetAsync();
            
            var jobFromHH = await _service.FindJobInHHAsync(page);           
           
            var jobs = jobFromDb.Union(jobFromHH).ToList();
                
            if (jobs == null)
            {
                return View(new PaginatedJobViewModel());
            }

            
            int totalJobs = jobs.Count;
            int totalPages = (int)Math.Ceiling(totalJobs / (double)pageSize);

            
            var jobsOnPage = jobs
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();


            var paginatedViewModel = new PaginatedJobViewModel
            {
                Jobs = jobsOnPage,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalJobs = jobs.Count()
            };

            return View(paginatedViewModel);
        }

        //[Authorize]
        [HttpGet("GetAllJobApi")]
        public async Task<IActionResult> GetAllApi()
        {
            var jobFromDb = await _repository.GetAsync();

            var jobFromHH = await _service.FindJobInHHAsync(3);

            var jobs = jobFromDb.Union(jobFromHH);
            if (jobs == null)
            {

                return NotFound();
            }

            return Ok(jobs);
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

            var jobModel = jobDTO.ToJobFromCreateDto();
            var res = await _repository.CreateAsync(jobModel);
            return CreatedAtAction(nameof(GetById), new { id = jobModel.Id }, jobModel.ToJobDto());
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string? searchString)
        {
            var jobs = await _repository.GetAsync();

            if (searchString == null)
            {
                return View("GetAll", jobs);
            }

            if (jobs == null || !jobs.Any())
            {
                ViewData["CurrentFilter"] = searchString;
                return View("GetAll", new List<Job>());
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                jobs = jobs.Where(j =>
                    j.Title.ToLower().Contains(searchString.ToLower()) ||
                    j.CompanyName.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            ViewData["CurrentFilter"] = searchString;

            return View("GetAll", new PaginatedJobViewModel() { 
                Jobs = jobs,
                TotalJobs = jobs.Count()
            } );
        }

        [Authorize]
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var jobModel = await _repository.GetByIdAsync(id);
            

            return View(jobModel);
        }





    }
}

